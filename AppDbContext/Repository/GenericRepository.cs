using AppDbContext.DBContext;
using AppDbContext.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppDbContext.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly GenericDbContext<T> _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(GenericDbContext<T> context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var data = _dbSet.Where(predicate);
            return await data.ToListAsync();
        }

        private async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                return false;
            }
        }
    }
}
