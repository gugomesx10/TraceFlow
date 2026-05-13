using Microsoft.EntityFrameworkCore;
using TraceFlowTraining.Domain.Entities;

namespace TraceFlowTraining.Infrastructure.Persistence.Context;

public class TraceFlowDbContext : DbContext
{
    public TraceFlowDbContext(DbContextOptions<TraceFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();
}
