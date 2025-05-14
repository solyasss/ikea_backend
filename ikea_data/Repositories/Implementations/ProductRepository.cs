using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ikea_data.Repositories.Implementations;
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly IkeaDbContext _db;
    public ProductRepository(IkeaDbContext db) : base(db) => _db = db;

    public async Task<Product?> GetDetailedAsync(int id) =>
        await _db.Products
            .Include(p => p.Category)
            .Include(p => p.Images)
            .Include(p => p.Characteristics)
            .Include(p => p.Comments).ThenInclude(c => c.User)
            .FirstOrDefaultAsync(p => p.Id == id);
}