using AutoMapper;
using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;
namespace ikea_business.Services.Implementations
{
    public class ProductCommentService : IProductCommentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProductCommentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {

            var list = await _uow.Comments.GetAllAsync();
            return list.Select(p => new
            {
              p.Id,
              p.ProductId,
              p.UserId,
              p.Rating,
             
            });
        }

        public async Task<object?> GetAsync(int id)
        {
            var p = await _uow.Comments.GetByIdAsync(id);
            if (p == null) return null;
            return new
            {
                p.Id,
              p.ProductId,
              p.UserId,
              p.Rating,
            };
        }

        public async Task<int> CreateAsync(ProductCommentInput dto)
        {
            var entity = _mapper.Map<ProductComment>(dto);
            await _uow.Comments.AddAsync(entity);
            await _uow.SaveAsync();
            return entity.Id;

            throw new NotImplementedException();
        }
        public async Task<bool> UpdateAsync(int id, ProductCommentInput dto)
        {
            var entity = await _uow.Comments.GetByIdAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            _uow.Comments.Update(entity);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _uow.Comments.GetByIdAsync(id);
            if (e == null) return false;
            _uow.Comments.Delete(e);
            await _uow.SaveAsync();
            return true;
        }

    }
}
