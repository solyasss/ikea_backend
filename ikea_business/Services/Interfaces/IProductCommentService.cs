using ikea_business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikea_business.Services.Interfaces
{
    public interface IProductCommentService
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<object?> GetAsync(int id);
        Task<int> CreateAsync(ProductCommentInput dto);
        Task<bool> UpdateAsync(int id, ProductCommentInput dto);    
        Task<bool> DeleteAsync(int id);
    }
}
