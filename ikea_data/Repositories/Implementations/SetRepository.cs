using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Implementations;
using ikea_data.Repositories.Interfaces;

public class SetRepository : GenericRepository<Set>, ISetRepository
{
    public SetRepository(IkeaDbContext db) : base(db) { }
}