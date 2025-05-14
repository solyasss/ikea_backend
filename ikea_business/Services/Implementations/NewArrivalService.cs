// NewArrivalService.cs
using AutoMapper;
using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_business.Services
{
    public class NewArrivalService : INewArrivalService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper     _map;
        public NewArrivalService(IUnitOfWork uow, IMapper map)
        {
            _uow  = uow;
            _map  = map;
        }

        public async Task<IEnumerable<NewArrival>> GetAllAsync() =>
            await _uow.NewArrivals.GetAllAsync();

        public async Task<NewArrival?> GetAsync(int id) =>
            await _uow.NewArrivals.GetByIdAsync(id);

        public async Task<int> CreateAsync(NewArrivalInput dto)
        {
            var entity = _map.Map<NewArrival>(dto);
            await _uow.NewArrivals.AddAsync(entity);
            await _uow.SaveAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, NewArrivalInput dto)
        {
            var e = await _uow.NewArrivals.GetByIdAsync(id);
            if (e == null) return false;
            _map.Map(dto, e);
            _uow.NewArrivals.Update(e);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _uow.NewArrivals.GetByIdAsync(id);
            if (e == null) return false;
            _uow.NewArrivals.Delete(e);
            await _uow.SaveAsync();
            return true;
        }
    }
}