using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Implementations;
using ikea_data.Repositories.Interfaces;

public class ProductCommentRepository : GenericRepository<ProductComment>, IProductCommentRepository
{
    public ProductCommentRepository(IkeaDbContext db) : base(db) { }
}