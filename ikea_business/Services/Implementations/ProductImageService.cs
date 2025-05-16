using AutoMapper;
using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_business.Services.Implementations
{
    public class ProductImageService : IProductImageService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductImageService(IUnitOfWork now, IMapper mapper) 
        {
            _uow = now;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {

            var list = await _uow.Images.GetAllAsync();
            return list.Select(p => new
            {
                p.Id,
                p.ProductId,
                p.ImageUrl,
                p.SortOrder
            });
        }

        public async Task<object?> GetAsync(int id)
        {
            var p = await _uow.Images.GetByIdAsync(id);
            if (p == null) return null;
            return new
            {
                p.Id,
                p.ProductId,
                p.ImageUrl,
                p.SortOrder
            };
        }

        public async Task<int> CreateAsync(ProductImageInput dto)
        {
            var entity = _mapper.Map<ProductImage>(dto);
            await _uow.Images.AddAsync(entity);
            await _uow.SaveAsync();
            return entity.Id;

            throw new NotImplementedException();
        }
        public async Task<bool> UpdateAsync(int id, ProductImageInput dto)
        {
            var entity = await _uow.Images.GetByIdAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            _uow.Images.Update(entity);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _uow.Images.GetByIdAsync(id);
            if (e == null) return false;
            _uow.Images.Delete(e);
            await _uow.SaveAsync();
            return true;
        }

    }
}
