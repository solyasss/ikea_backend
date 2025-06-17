using ikea_business.DTO;

namespace ikea_business.Services.Interfaces;

public interface IProductService
{
    Task<(IEnumerable<object> items, int totalCount)> GetPagedAsync(int page, int pageSize);
    Task<IEnumerable<object>> GetAllAsync();
    Task<object?>             GetAsync(int id);
    Task<int>                 CreateAsync(ProductInput dto);
    Task<bool>                UpdateAsync(int id, ProductInput dto);
    Task<bool>                DeleteAsync(int id);
    Task<IEnumerable<object>> SearchByNameAsync(string name);


}
