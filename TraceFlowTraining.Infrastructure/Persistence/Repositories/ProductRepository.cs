using Microsoft.EntityFrameworkCore;
using TraceFlowTraining.Domain.Entities;
using TraceFlowTraining.Domain.Interfaces;
using TraceFlowTraining.Infrastructure.Persistence.Context;

namespace TraceFlowTraining.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly TraceFlowDbContext _context;

    public ProductRepository(TraceFlowDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task CreateAsync(Product product)
    {
        await _context.Products.AddAsync(product);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
            return;

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
    }
}