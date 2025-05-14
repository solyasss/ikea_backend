using ikea_business.Services.Interfaces;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        public CategoryService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<object>> GetTreeAsync()
        {
            var root = await _uow.Categories.FindAsync(c => c.ParentId == null);
            return root.Select(c => new
            {
                c.Id,
                c.Title,
                c.Slug,
                Children = c.Children.Select(sc => new { sc.Id, sc.Title, sc.Slug })
            });
        }

        public async Task<object?> GetAsync(int id)
        {
            var c = await _uow.Categories.GetByIdAsync(id);
            if (c == null) return null;
            return new
            {
                c.Id,
                c.Title,
                c.Slug,
                Children = c.Children.Select(sc => new { sc.Id, sc.Title, sc.Slug })
            };
        }

        public async Task<int> CreateAsync(Category cat)
        {
            await _uow.Categories.AddAsync(cat);
            await _uow.SaveAsync();
            return cat.Id;
        }

        public async Task<bool> UpdateAsync(int id, Category cat)
        {
            var e = await _uow.Categories.GetByIdAsync(id);
            if (e == null) return false;
            e.Title = cat.Title;
            e.Slug = cat.Slug;
            e.ParentId = cat.ParentId;
            _uow.Categories.Update(e);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _uow.Categories.GetByIdAsync(id);
            if (e == null) return false;
            _uow.Categories.Delete(e);
            await _uow.SaveAsync();
            return true;
        }
    }
}