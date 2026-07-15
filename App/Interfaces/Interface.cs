using IoTProject.App.DTOs;
using IoTProject.App.Services;
using IoTProject.Domain;
using System.Security.Claims;
namespace IoTProject.App.Interfaces
{
    public interface ITelemetryService
    {
        Task PostTelemetry(TelemetryEntry telemetryRecord);
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
    public interface IJWTService
    {
        string GenerateToken(Guid userId);
        ClaimsPrincipal ValidateToken(string token);
    }
}
