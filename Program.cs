
using IoTProject.App.Interfaces;
using IoTProject.App.Services;
using IoTProject.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using IoTProject.Api;
using Microsoft.AspNetCore.Builder;
namespace IoTProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers + OpenAPI binding
            builder.Services.AddControllers();

            // Db + services
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ITelemetryService, TelemetryService>();
            builder.Services.AddScoped<IDeviceService, DeviceService>();
            builder.Services.AddScoped<IAuthService,AuthService>();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.MapOpenApi();
            
            app.Run();
        }
    }
}
