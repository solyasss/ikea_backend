using AutoMapper;
using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_business.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            var list = await _uow.Products.GetAllAsync();
            return list.Select(p => new
            {
                p.Id,
                p.Article,
                p.Name,
                p.Price,
                p.MainImage
            });
        }

        public async Task<object?> GetAsync(int id)
        {
            var p = await _uow.Products.GetDetailedAsync(id);
            if (p == null) return null;
            return new
            {
                p.Id,
                p.Article,
                p.Name,
                p.Price,
                p.MainImage,
                p.Color,
                p.Dimensions,
                p.Weight,
                p.Type,
                p.CountryOfOrigin,
                p.PackageContents,
                p.Warranty,
                p.Materials,
                p.Rating,
                Category = new { p.Category!.Id, p.Category.Title },
                Images = p.Images.OrderBy(i => i.SortOrder).Select(i => i.ImageUrl),
                Characteristics = p.Characteristics.Select(c => new { c.Name, c.Value }),
                Comments = p.Comments.Select(c => new
                {
                    c.Id,
                    c.CommentText,
                    c.Rating,
                    User = new { c.User!.Id, c.User.FirstName, c.User.LastName }
                })
            };
        }

        public async Task<int> CreateAsync(ProductInput dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _uow.Products.AddAsync(entity);
            await _uow.SaveAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, ProductInput dto)
        {
            var entity = await _uow.Products.GetByIdAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            _uow.Products.Update(entity);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _uow.Products.GetByIdAsync(id);
            if (e == null) return false;
            _uow.Products.Delete(e);
            await _uow.SaveAsync();
            return true;
        }
    }
}
