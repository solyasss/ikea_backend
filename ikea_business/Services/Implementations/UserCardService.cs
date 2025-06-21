using AutoMapper;
using ikea_business.DTO;
using ikea_business.Helpers;
using ikea_business.Services.Interfaces;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_business.Services;

public class UserCardService : IUserCardService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper     _map;

    public UserCardService(IUnitOfWork uow, IMapper map)
    {
        _uow = uow;
        _map = map;
    }

    public async Task<IEnumerable<object>> GetAllAsync()
    {
        var list = await _uow.UserCards.GetAllAsync();
        return list.Select(c => new
        {
            c.Id, c.UserId, c.CardNumber,
            c.ValidDay, c.ValidYear, c.CardType
        });
    }

    public async Task<object?> GetAsync(int id)
    {
        var c = await _uow.UserCards.GetByIdAsync(id);
        return c == null ? null : new
        {
            c.Id, c.UserId, c.CardNumber,
            c.ValidDay, c.ValidYear, c.CardType
        };
    }

    public async Task<int> CreateAsync(UserCardInput dto)
    {
        var entity        = _map.Map<UserCard>(dto);
        entity.CvvHash    = CvvHasher.Hash(dto.Cvv);

        await _uow.UserCards.AddAsync(entity);
        await _uow.SaveAsync();
        return entity.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var e = await _uow.UserCards.GetByIdAsync(id);
        if (e == null) return false;
        _uow.UserCards.Delete(e);
        await _uow.SaveAsync();
        return true;
    }

    public async Task<object?> GetByUserIdAsync(int userId)
    {
        var card = await _uow.UserCards.GetByUserIdAsync(userId); 

        return card == null ? null : new
        {
            card.Id,
            card.UserId,
            card.CardNumber,
            card.ValidDay,
            card.ValidYear,
            card.CardType
        };
    }


}