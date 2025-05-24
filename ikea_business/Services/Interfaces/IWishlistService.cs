using ikea_business.DTO;

namespace ikea_business.Services.Interfaces;

public interface IWishlistService
{
    Task<IEnumerable<object>> GetAllAsync();
    Task<object?>             GetAsync(int id);
    Task<int>                 CreateAsync(WishlistInput dto);
    Task<bool>                DeleteAsync(int id);
}