using AutoMapper;
using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_business.Services.Implementations;

public class WishlistService : IWishlistService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper     _map;

    public WishlistService(IUnitOfWork uow, IMapper map)
    {
        _uow = uow;
        _map = map;
    }

    public async Task<IEnumerable<object>> GetAllAsync()
    {
        var list = await _uow.Wishlists.GetAllAsync();
        return list.Select(w => new { w.Id, w.UserId, w.ProductId });
    }

    public async Task<object?> GetAsync(int id)
    {
        var w = await _uow.Wishlists.GetByIdAsync(id);
        return w == null ? null : new { w.Id, w.UserId, w.ProductId };
    }

    public async Task<int> CreateAsync(WishlistInput dto)
    {
        var entity = _map.Map<Wishlist>(dto);
        await _uow.Wishlists.AddAsync(entity);
        await _uow.SaveAsync();
        return entity.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var e = await _uow.Wishlists.GetByIdAsync(id);
        if (e == null) return false;
        _uow.Wishlists.Delete(e);
        await _uow.SaveAsync();
        return true;
    }
}