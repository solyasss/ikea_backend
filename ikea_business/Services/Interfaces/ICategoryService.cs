using ikea_data.Models;

namespace ikea_business.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<object>> GetTreeAsync();
    Task<object?>             GetAsync(int id);
    Task<int>                 CreateAsync(Category cat);
    Task<bool>                UpdateAsync(int id, Category cat);
    Task<bool>                DeleteAsync(int id);
}