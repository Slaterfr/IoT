using System.Text.Json;

namespace IoTProject.Domain.Entities
{
    public class TelemetryRecord
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string TelemetryType { get; set; }
        public JsonDocument payload { get; set; }

        public DateTime Timestamp { get; set; }

        public Device Device { get; set; } = null!;
    }
}
