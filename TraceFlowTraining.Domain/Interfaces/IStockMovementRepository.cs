using TraceFlowTraining.Domain.Entities;

namespace TraceFlowTraining.Domain.Interfaces;

public interface IStockMovementRepository
{
    Task<IEnumerable<StockMovement>> GetAllAsync();

    Task<StockMovement?> GetByIdAsync(Guid id);

    Task CreateAsync(StockMovement stockMovement);

    Task DeleteAsync(Guid id);
}