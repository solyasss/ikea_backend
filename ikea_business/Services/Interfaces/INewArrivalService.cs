using ikea_business.DTO;
using ikea_data.Models;

namespace ikea_business.Services.Interfaces;

public interface INewArrivalService
{
    Task<IEnumerable<NewArrival>> GetAllAsync();
    Task<NewArrival?>             GetAsync(int id);
    Task<int>                     CreateAsync(NewArrivalInput dto);
    Task<bool>                    UpdateAsync(int id, NewArrivalInput dto);
    Task<bool>                    DeleteAsync(int id);
}