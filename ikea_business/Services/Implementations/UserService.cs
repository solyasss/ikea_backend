using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ikea_business.DTO;
using ikea_business.Helpers;
using ikea_business.Services.Interfaces;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper     _map;
        public UserService(IUnitOfWork uow, IMapper map)
        {
            _uow = uow;
            _map = map;
        }

        public async Task<(IEnumerable<object> items, int totalCount)> GetPagedAsync(int page, int pageSize)
        {
            var all = await _uow.Users.GetAllAsync();
            var totalCount = all.Count();
            var items = all
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new
                {
                    u.Id,
                    u.IsAdmin,
                    u.FirstName,
                    u.LastName,
                    u.Email
                })
                .ToList();
            return (items, totalCount);
        }

        public Task<IEnumerable<object>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<object?> GetAsync(int id)
        {
            var u = await _uow.Users.GetByIdAsync(id);
            if (u == null) return null;
            var cards = await _uow.UserCards.FindAsync(c => c.UserId == id);
            var userCards = cards.Select(c => new
            {
                c.Id,
                c.CardNumber,
                c.ValidDay,
                c.ValidYear,
                c.CardType
            });
            return new
            {
                u.Id,
                u.IsAdmin,
                u.FirstName,
                u.LastName,
                u.BirthDate,
                u.Country,
                u.Address,
                u.Phone,
                u.Email,
                Cards = userCards
            };
        }

        public async Task<int> CreateAsync(UserInput dto)
        {
            var entity = _map.Map<User>(dto);
            PasswordHasher.CreateHash(dto.Password, out var hash, out var salt);
            entity.PasswordHash = hash;
            entity.PasswordSalt = salt;
            await _uow.Users.AddAsync(entity);
            await _uow.SaveAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateInput dto)
        {
            var e = await _uow.Users.GetByIdAsync(id);
            if (e == null) return false;
            _map.Map(dto, e);
            _uow.Users.Update(e);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<bool> ChangePasswordAsync(int id, string newPassword)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null) return false;
            PasswordHasher.CreateHash(newPassword, out var hash, out var salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            _uow.Users.Update(user);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _uow.Users.GetByIdAsync(id);
            if (e == null) return false;
            _uow.Users.Delete(e);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var users = await _uow.Users.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user == null) return null;
            return PasswordHasher.Verify(password, user.PasswordHash, user.PasswordSalt) ? user : null;
        }
    }
}
