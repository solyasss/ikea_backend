using ikea_business.DTO;

namespace ikea_business.Services.Interfaces;

public interface ICartService
{
    Task<IEnumerable<object>> GetAllAsync();
    Task<object?>             GetAsync(int id);
    Task<int>                 CreateAsync(CartInput dto);
    Task<bool>                DeleteAsync(int id);
}