using ikea_business.DTO;
using ikea_data.Models;

namespace ikea_business.Services.Interfaces;

public interface IUserService
{
    Task<(IEnumerable<object> items, int totalCount)> GetPagedAsync(int page, int pageSize);
    
    Task<object?>             GetAsync(int id);
    Task<int>                 CreateAsync(UserInput dto);
    Task<bool>                UpdateAsync(int id, UserUpdateInput dto);
    Task<bool>                DeleteAsync(int id);
    Task<bool> ChangePasswordAsync(int id, string newPassword);
    Task<User?> AuthenticateAsync(string email, string password);
  

}