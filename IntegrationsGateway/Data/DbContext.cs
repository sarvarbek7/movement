using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Movement.IntegrationsGateway.Data;

public sealed class IntegrationsGatewayDbContext(DbContextOptions<IntegrationsGatewayDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}