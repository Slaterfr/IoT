using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IoTProject.App.Interfaces;
using IoTProject.App.DTOs;
using IoTProject.Domain.Enums;

namespace IoTProject.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TelemetryController : ControllerBase
{
    private readonly ITelemetryService _telemetryservice;

    public TelemetryController(ITelemetryService telemetryservice)
    {
        _telemetryservice = telemetryservice;
    }

    [HttpPost]
    public async Task<IActionResult> PostTelemetry([FromBody] TelemetryEntry datos, [FromHeader(Name = "X-Api-Key")] string key)
    {
        await _telemetryservice.PostTelemetry(datos, key);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetLast10Records([FromRoute] Guid? guid)
    {
        var readings = await _telemetryservice.GetLastRecords(guid);
        return Ok(readings);
    }
}
