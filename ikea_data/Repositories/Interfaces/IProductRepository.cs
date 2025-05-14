using ikea_data.Models;

namespace ikea_data.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetDetailedAsync(int id);
    }
}