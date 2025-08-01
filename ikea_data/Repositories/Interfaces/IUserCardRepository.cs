using ikea_data.Models;

namespace ikea_data.Repositories.Interfaces;

public interface IUserCardRepository : IGenericRepository<UserCard>
{
    Task<UserCard?> GetByUserIdAsync(int userId);
}