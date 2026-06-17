using IoTProject.Domain.Enums;
using IoTProject.Domain.Entities;

namespace IoTProject.Domain.Entities
{
    public class Device
    {
        public Guid Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<TelemetryRecord> TelemetryRecords { get; set; }
                = new();
        public DeviceStatus Status { get; set; } = DeviceStatus.Online;
        public DateTime LastSeen { get; set; }
        public string ApiKey { get; set; }
      }
   }
