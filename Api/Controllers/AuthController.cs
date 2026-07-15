using IoTProject.App.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IoTProject.App.DTOs;
using Microsoft.AspNetCore.Authorization;
using IoTProject.Domain.Entities;
using System.Security.Claims;
using NuGet.Common;
namespace IoTProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest data)
        {

            var token = await _authService.Login(data);

            return Ok(token);
        }


    }
}
