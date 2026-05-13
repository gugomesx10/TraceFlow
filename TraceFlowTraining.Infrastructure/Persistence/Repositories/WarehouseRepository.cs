using Microsoft.EntityFrameworkCore;
using TraceFlowTraining.Domain.Entities;
using TraceFlowTraining.Domain.Interfaces;
using TraceFlowTraining.Infrastructure.Persistence.Context;

namespace TraceFlowTraining.Infrastructure.Persistence.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly TraceFlowDbContext _context;

    public WarehouseRepository(TraceFlowDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Warehouse>> GetAllAsync()
    {
        return await _context.Warehouses.ToListAsync();
    }

    public async Task<Warehouse?> GetByIdAsync(Guid id)
    {
        return await _context.Warehouses.FindAsync(id);
    }

    public async Task CreateAsync(Warehouse warehouse)
    {
        await _context.Warehouses.AddAsync(warehouse);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Warehouse warehouse)
    {
        _context.Warehouses.Update(warehouse);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var warehouse = await _context.Warehouses.FindAsync(id);

        if (warehouse is null)
            return;

        _context.Warehouses.Remove(warehouse);

        await _context.SaveChangesAsync();
    }
}