using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_data.Repositories.Implementations;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IkeaDbContext db) : base(db) { }
}