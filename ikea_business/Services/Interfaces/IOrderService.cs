using ikea_business.DTO;

namespace ikea_business.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<object>> GetAllAsync();
    Task<object?>             GetAsync(int id);
    Task<int>                 CreateAsync(OrderInput dto);
    Task<bool>                DeleteAsync(int id);
}