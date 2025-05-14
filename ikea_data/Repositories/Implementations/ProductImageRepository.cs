using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Implementations;
using ikea_data.Repositories.Interfaces;

public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(IkeaDbContext db) : base(db) { }
}