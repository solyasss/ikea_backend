using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ikea_data.Repositories.Implementations;

public class UserCardRepository : GenericRepository<UserCard>, IUserCardRepository
{
    private readonly IkeaDbContext _context;

    public UserCardRepository(IkeaDbContext db) : base(db)
    {
        _context = db;
    }

    public async Task<UserCard?> GetByUserIdAsync(int userId)
    {
        return await _context.UserCards
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }
}