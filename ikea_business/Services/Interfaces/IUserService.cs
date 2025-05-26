using ikea_business.DTO;

namespace ikea_business.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<object>> GetAllAsync();
    Task<object?>             GetAsync(int id);
    Task<int>                 CreateAsync(UserInput dto);
    Task<bool>                UpdateAsync(int id, UserUpdateInput dto);
    Task<bool>                DeleteAsync(int id);
    Task<bool> ChangePasswordAsync(int id, string newPassword);

}