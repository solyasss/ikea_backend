using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Implementations;
using ikea_data.Repositories.Interfaces;

public class NewArrivalRepository : GenericRepository<NewArrival>, INewArrivalRepository
{
    public NewArrivalRepository(IkeaDbContext db) : base(db) { }
}