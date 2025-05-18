using AutoMapper;
using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikea_business.Services.Implementations
{
    public class ProductCharacteristicService: IProductCharacteristicService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProductCharacteristicService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            var list = await _uow.Characteristics.GetAllAsync();
            return list.Select(p => new
            {
                p.Id,
                p.ProductId,
                p.Name,
                p.Value
               
            });
        }

        public async Task<object?> GetAsync(int id)
        {
            var p = await _uow.Characteristics.GetByIdAsync(id);
            if (p == null) return null;
            return new
            {
                p.Id,
                p.ProductId,
                p.Name,
                p.Value
            };
        }
        public async Task<int> CreateAsync(ProductCharacteristicInput dto)
        {
            var entity = _mapper.Map<ProductCharacteristic> (dto);
            await _uow.Characteristics.AddAsync(entity);
            await _uow.SaveAsync();
            return entity.Id;

            throw new NotImplementedException();
        }
        public async Task<bool> UpdateAsync(int id, ProductCharacteristicInput dto)
        {
            var entity = await _uow.Characteristics.GetByIdAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            _uow.Characteristics.Update(entity);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _uow.Characteristics.GetByIdAsync(id);
            if (e == null) return false;
            _uow.Characteristics.Delete(e);
            await _uow.SaveAsync();
            return true;
        }

    }
}
