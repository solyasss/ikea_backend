using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Implementations;
using ikea_data.Repositories.Interfaces;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(IkeaDbContext db) : base(db) { }
}