using ikea_business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikea_business.Services.Interfaces
{
    public interface IProductCharacteristicService
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<object?> GetAsync(int id);
        Task<int> CreateAsync(ProductCharacteristicInput dto);
        Task<bool> UpdateAsync(int id, ProductCharacteristicInput dto); 
        Task<bool> DeleteAsync(int id);
    }
}
