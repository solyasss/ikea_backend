using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_data.Repositories.Implementations;

public class CartRepository : GenericRepository<Cart>, ICartRepository
{
    public CartRepository(IkeaDbContext db) : base(db) { }
}