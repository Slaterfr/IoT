using Microsoft.AspNetCore.Mvc;
using IoTProject.Infrastructure;
using IoTProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using IoTProject.App.DTOs;
using IoTProject.App.Interfaces;
using IoTProject.Domain.Enums;
using Microsoft.CodeAnalysis.Operations;

namespace IoTProject.App.Services
{
    public class TelemetryService : ITelemetryService
    {
        private readonly AppDbContext _context;

        public TelemetryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TelemetryRead>> GetLastRecords(Guid? DeviceId)
        {
            var query = _context.TelemetryRecord.AsQueryable();

            if (DeviceId.HasValue)
            {
                query = query.Where(x => x.DeviceId == DeviceId.Value);
            }
            
            var readings = await query.OrderByDescending(x => x.Timestamp).Take(10).Select(x => new TelemetryRead
            {
                DeviceId = DeviceId.HasValue ? DeviceId.Value : x.DeviceId,
                Timestamp = x.Timestamp,
                TelemetryType = x.TelemetryType,
                payload = x.payload
            }).ToListAsync(); 

            return readings;
        }


        public async Task PostTelemetry(TelemetryEntry data, string ApiKey)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(x => x.Id == data.DeviceId);

            if (device == null)
            {
                throw new Exception("Device not found");
            }

            if (device.ApiKey != ApiKey)
            {
                throw new Exception("unvalid auth");
            }

            device.Status = DeviceStatus.Online;
            device.LastSeen = DateTime.UtcNow;
            
            var record = new TelemetryRecord
            {
                DeviceId = data.DeviceId,
                TelemetryType = data.TelemetryType,
                payload = data.payload,
                Timestamp = data.Timestamp
            };

            await _context.TelemetryRecord.AddAsync(record);

            await _context.SaveChangesAsync();


        }

    }
}
