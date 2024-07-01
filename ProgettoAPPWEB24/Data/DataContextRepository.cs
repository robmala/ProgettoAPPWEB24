using Microsoft.EntityFrameworkCore;
using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Data
{
    public class DataContextRepository<T> : IRepository<T>
        where T : class, IHasId
    {
        readonly DbContext _context;
        readonly DbSet<T> _set;

        public DataContextRepository(DbContext dataContext)
        {
            ArgumentNullException.ThrowIfNull(dataContext);
            
            _context = dataContext;
            _set = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await Get(id);
            if (entity != null)
            {
                _set.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public IAsyncEnumerable<T> GetAll()
        {
            return _set.AsAsyncEnumerable();
        }

        public async Task<T?> Get(int id)
        {
            return await _context.FindAsync<T>(id);
        }
    }
}
