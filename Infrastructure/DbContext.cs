using Microsoft.EntityFrameworkCore;
using IoTProject.Domain.Entities;

namespace IoTProject.Infrastructure;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Device> Devices => Set<Device>();
    public DbSet<TelemetryRecord> TelemetryRecord => Set<TelemetryRecord>();
    public DbSet<User> Users => Set<User>();
}