using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using IoTProject.Domain.Entities;
using IoTProject.Domain.Enums;
using IoTProject.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
namespace IoTProject.App.DTOs;


public class TelemetryEntry
{
 
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string ApiKey { get; set; }

        [Required]
        public string TelemetryType { get; set; }
        public JsonDocument payload { get; set; }
        public DateTime Timestamp { get; set; }
}

public class TelemetryRead {

    public Guid DeviceId { get; set; }
    public string DeviceName { get; set; }
    public string TelemetryType { get; set; }
    public JsonDocument payload { get; set; }
    public DateTime Timestamp { get; set; }
    public Device device { get; set; }
}
public class DeviceEntry
{
        [Required]
        public string Identifier { get; set;}
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DeviceStatus Devicestatus { get; set; }
        
}

public class DeviceRead
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public DeviceStatus Devicestatus { get; set; }
    [Required]
    public string ApiKey { get; set; }
}


public class RegisterRequest
{
    [Required]
    public string Name { get; set;}
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}


public class LoginRequest
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}