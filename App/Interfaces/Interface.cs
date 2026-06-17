using IoTProject.App.Services;
using IoTProject.Domain;
using IoTProject.App.DTOs;
namespace IoTProject.App.Interfaces
{
    public interface ITelemetryService
    {
        Task PostTelemetry(TelemetryEntry telemetryRecord, string ApiKey);
        Task<List<TelemetryRead>> GetLastRecords(Guid? DeviceId);

    }

    public interface IDeviceService
    {
        Task PostDevice(DeviceEntry device);
        Task<List<DeviceRead>> GetDevices();

    }

    public interface IAuthService
    {
        Task Register(RegisterRequest data);
        Task<string> Login(LoginRequest data);


    }
}
