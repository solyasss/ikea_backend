using AutoMapper;
using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_business.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper     _map;

    public OrderService(IUnitOfWork uow, IMapper map)
    {
        _uow = uow;
        _map = map;
    }

    public async Task<IEnumerable<object>> GetAllAsync()
    {
        var orders = await _uow.Orders.GetAllAsync();

        var result = new List<object>();

        foreach (var o in orders)
        {
            var product = await _uow.Products.GetByIdAsync(o.ProductId);
            var comment = (await _uow.Comments.GetAllAsync())
                .FirstOrDefault(c => c.UserId == o.UserId && c.ProductId == o.ProductId);

            result.Add(new
            {
                o.Id,
                o.OrderNumber,
                o.UserId,
                o.ProductId,
                ProductName = product?.Name,
                ProductImage = product?.MainImage,
                ProductPrice = product?.Price,
                CommentText = comment?.CommentText,
                CommentRating = comment?.Rating,
                o.OrderDate,
                o.ReceiveDate,
                o.IsCash,
                o.TotalSum
            });
        }

        return result;
    }

    public async Task<object?> GetAsync(int id)
    {
        var o = await _uow.Orders.GetByIdAsync(id);
        if (o == null) return null;

        var product = await _uow.Products.GetByIdAsync(o.ProductId);
        var comment = (await _uow.Comments.GetAllAsync())
            .FirstOrDefault(c => c.UserId == o.UserId && c.ProductId == o.ProductId);

        return new
        {
            o.Id,
            o.OrderNumber,
            o.UserId,
            o.ProductId,
            ProductName = product?.Name,
            ProductImage = product?.MainImage,
            ProductPrice = product?.Price,
            CommentText = comment?.CommentText,
            CommentRating = comment?.Rating,
            o.OrderDate,
            o.ReceiveDate,
            o.IsCash,
            o.TotalSum
        };
    }

    public async Task<int> CreateAsync(OrderInput dto)
    {
        var entity = _map.Map<Order>(dto);
        await _uow.Orders.AddAsync(entity);
        await _uow.SaveAsync();
        return entity.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var e = await _uow.Orders.GetByIdAsync(id);
        if (e == null) return false;
        _uow.Orders.Delete(e);
        await _uow.SaveAsync();
        return true;
    }

    public async Task<IEnumerable<object>> GetByUserIdAsync(int userId)
    {
        var orders = (await _uow.Orders.GetAllAsync())
            .Where(o => o.UserId == userId)
            .ToList();

        var result = new List<object>();

        foreach (var o in orders)
        {
            var product = await _uow.Products.GetByIdAsync(o.ProductId);
            var comment = (await _uow.Comments.GetAllAsync())
                .FirstOrDefault(c => c.UserId == o.UserId && c.ProductId == o.ProductId);

            result.Add(new
            {
                o.Id,
                o.OrderNumber,
                o.UserId,
                o.ProductId,
                ProductName = product?.Name,
                ProductImage = product?.MainImage,
                ProductPrice = product?.Price,
                CommentText = comment?.CommentText,
                CommentRating = comment?.Rating,
                o.OrderDate,
                o.ReceiveDate,
                o.IsCash,
                o.TotalSum
            });
        }

        return result;
    }

}
