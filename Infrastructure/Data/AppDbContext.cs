using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Movement.Infrastructure.Data;

public class MovementDbContext(DbContextOptions<MovementDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplyConfigurations(modelBuilder);
    }

    private static void ApplyConfigurations(ModelBuilder builder)
  {
    var currentAssembly = Assembly.GetAssembly(typeof (MovementDbContext))!;

    builder.ApplyConfigurationsFromAssembly(currentAssembly);
  }
}