using AutoMapper;
using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_business.Services.Implementations;

public class CartService : ICartService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper     _map;

    public CartService(IUnitOfWork uow, IMapper map)
    {
        _uow = uow;
        _map = map;
    }

    public async Task<IEnumerable<object>> GetAllAsync()
    {
        var list = await _uow.Carts.GetAllAsync();
        return list.Select(c => new
        {
            c.Id, c.UserId, c.ProductId,
            c.Quantity, c.IsCash, c.TotalSum
        });
    }

    public async Task<object?> GetAsync(int id)
    {
        var c = await _uow.Carts.GetByIdAsync(id);
        return c == null ? null : new
        {
            c.Id, c.UserId, c.ProductId,
            c.Quantity, c.IsCash, c.TotalSum
        };
    }

    public async Task<int> CreateAsync(CartInput dto)
    {
        var entity = _map.Map<Cart>(dto);
        await _uow.Carts.AddAsync(entity);
        await _uow.SaveAsync();
        return entity.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var e = await _uow.Carts.GetByIdAsync(id);
        if (e == null) return false;
        _uow.Carts.Delete(e);
        await _uow.SaveAsync();
        return true;
    }
}