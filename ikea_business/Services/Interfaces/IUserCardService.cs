using ikea_business.DTO;

namespace ikea_business.Services.Interfaces;

public interface IUserCardService
{
    Task<IEnumerable<object>> GetAllAsync();
    Task<object?>             GetAsync(int id);
    Task<int>                 CreateAsync(UserCardInput dto);
    Task<bool>                DeleteAsync(int id);
    Task<object?> GetByUserIdAsync(int userId);

}