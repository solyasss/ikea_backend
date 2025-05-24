using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_data.Repositories.Implementations;

public class WishlistRepository : GenericRepository<Wishlist>, IWishlistRepository
{
    public WishlistRepository(IkeaDbContext db) : base(db) { }
}