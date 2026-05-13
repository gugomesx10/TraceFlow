using Microsoft.EntityFrameworkCore;
using TraceFlowTraining.Domain.Entities;
using TraceFlowTraining.Domain.Interfaces;
using TraceFlowTraining.Infrastructure.Persistence.Context;

namespace TraceFlowTraining.Infrastructure.Persistence.Repositories;

public class StockMovementRepository : IStockMovementRepository
{
    private readonly TraceFlowDbContext _context;

    public StockMovementRepository(TraceFlowDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StockMovement>> GetAllAsync()
    {
        return await _context.StockMovements
            .Include(sm => sm.Product)
            .ToListAsync();
    }

    public async Task<StockMovement?> GetByIdAsync(Guid id)
    {
        return await _context.StockMovements
            .Include(sm => sm.Product)
            .FirstOrDefaultAsync(sm => sm.Id == id);
    }

    public async Task CreateAsync(StockMovement stockMovement)
    {
        await _context.StockMovements.AddAsync(stockMovement);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var stockMovement = await _context.StockMovements.FindAsync(id);

        if (stockMovement is null)
            return;

        _context.StockMovements.Remove(stockMovement);

        await _context.SaveChangesAsync();
    }
}