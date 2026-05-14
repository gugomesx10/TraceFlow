using TraceFlowTraining.Domain.Entities;

namespace TraceFlowTraining.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(Guid id);

    Task CreateAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Guid id);
}