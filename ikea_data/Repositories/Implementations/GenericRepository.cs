using ikea_data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ikea_data.Repositories.Implementations;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> _set;
    protected readonly DbContext _ctx;
    public GenericRepository(DbContext ctx)
    {
        _ctx = ctx;
        _set = ctx.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync() =>
        await _set.ToListAsync();

    public async Task<TEntity?> GetByIdAsync(object id) =>
        await _set.FindAsync(id);

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
        await _set.Where(predicate).ToListAsync();

    public async Task AddAsync(TEntity entity) => await _set.AddAsync(entity);
    public void Update(TEntity entity) => _set.Update(entity);
    public void Delete(TEntity entity) => _set.Remove(entity);
    
   //
}