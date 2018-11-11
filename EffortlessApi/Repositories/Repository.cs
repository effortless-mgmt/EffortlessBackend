using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Repositories 
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        protected readonly DbContext _context;

        public Repository(DbContext context) => _context = context;

        public async Task AddAsync(TEntity entity) 
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) 
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) 
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() 
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(long id) 
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task Remove(TEntity entity) 
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<TEntity> entities) 
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}