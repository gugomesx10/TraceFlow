using TraceFlowTraining.Domain.Entities;

namespace TraceFlowTraining.Domain.Interfaces;

public interface IWarehouseRepository
{
    Task<IEnumerable<Warehouse>> GetAllAsync();

    Task<Warehouse?> GetByIdAsync(Guid id);

    Task CreateAsync(Warehouse warehouse);

    Task UpdateAsync(Warehouse warehouse);

    Task DeleteAsync(Guid id);
}