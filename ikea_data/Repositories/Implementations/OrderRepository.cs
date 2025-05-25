using ikea_data.Data;
using ikea_data.Models;
using ikea_data.Repositories.Interfaces;

namespace ikea_data.Repositories.Implementations;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(IkeaDbContext db) : base(db) { }
}