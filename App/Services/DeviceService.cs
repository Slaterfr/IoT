using Microsoft.AspNetCore.Mvc;
using IoTProject.Infrastructure;
using IoTProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using IoTProject.App.DTOs;
using IoTProject.App.Interfaces;
using IoTProject.Domain.Enums;
using IoTProject.App.Dependencies;
namespace IoTProject.App.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly AppDbContext _context;

        public DeviceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task PostDevice(DeviceEntry data)
        {
            var DeviceKey = ApiKeyGenerator.GenerateApiKey();
            var device = new Device 
            {
                Name = data.Name,
                Status = data.Devicestatus,
                ApiKey = DeviceKey
            };
            Console.WriteLine(DeviceKey);
            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<DeviceRead>> GetDevices()
        {
            var devices = await _context.Devices.Select(x => new DeviceRead
            {
                Id = x.Id, 
                Name = x.Name,
                Devicestatus = x.Status, ApiKey = x.ApiKey
            }).ToListAsync();

            return devices;
        }

        public async Task GetOneDevice(Guid ID)
        {
            var device = await _context.Devices.FindAsync(ID);
        }
    }
}
