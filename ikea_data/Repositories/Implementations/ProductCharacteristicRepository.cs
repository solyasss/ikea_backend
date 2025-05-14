using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Implementations;
using ikea_data.Repositories.Interfaces;

public class ProductCharacteristicRepository : GenericRepository<ProductCharacteristic>, IProductCharacteristicRepository
{
    public ProductCharacteristicRepository(IkeaDbContext db) : base(db) { }
}