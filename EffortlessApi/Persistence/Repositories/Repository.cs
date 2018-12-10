using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;

        /// <summary>
        /// Created a new data repository.
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context) => _context = context;

        /// <summary>
        /// Fetches all <see cref="T"/> entities in the database.
        /// </summary>
        /// <returns>All elements in the context.</returns>
        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Fetches a specific <see cref="T"/> item in the database. 
        /// </summary>
        /// <param name="id">The id of the element</param>
        /// <returns>The element with the given <see cref="id"/>.</returns>
        public async virtual Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Finds all <see cref="T"/> entities that matches the given <see cref="predicate"/>.
        /// </summary>
        /// <param name="predicate">
        /// The predicate to find the entity, for example:
        /// <code>await entity.FindAsync(x => x.createdBy == someUser);</code>
        /// </param>
        /// <returns>A list of entities that mateches the predicate.</returns>
        public async virtual Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Adds a <see cref="T"/> entity to the database.
        /// </summary>
        /// <param name="entity"></param>
        public async virtual Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        /// <summary>
        /// Adds multiple <see cref="T"/> entities to the database.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async virtual Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        /// <summary>
        /// Updates non-key fields of an existing entity with those specified in <see cref="newEntity"/>.
        /// </summary>
        /// <param name="newEntity">A <see cref="T"/> entity containing the updated fields.</param>
        public abstract Task UpdateAsync(T newEntity);

        /// <summary>
        /// Removes a specific <see cref="T"/> entity from the database.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Removes a range of specified <see cref="T"/> entities from the database.
        /// </summary>
        /// <param name="entities"></param>
        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}