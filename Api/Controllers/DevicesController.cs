using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IoTProject.App.Interfaces;
using IoTProject.App.DTOs;
using IoTProject.Domain.Enums;

namespace IoTProject.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DevicesController : ControllerBase
{
    private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpPost]
   public async Task<IActionResult> PostDevice([FromBody] DeviceEntry data)
    {
        await _deviceService.PostDevice(data);
        return Ok(data);

    }

    [HttpGet]
    public async Task<IActionResult> GetDevices()
    {
        var devices = await _deviceService.GetDevices();

        return Ok(devices);
    }
}

