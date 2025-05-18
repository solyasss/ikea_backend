using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_data.Repositories.Implementations;

public class UserCardRepository : GenericRepository<UserCard>, IUserCardRepository
{
    public UserCardRepository(IkeaDbContext db) : base(db) { }
}