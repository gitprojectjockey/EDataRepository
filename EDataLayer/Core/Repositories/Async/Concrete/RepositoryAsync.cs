using System;
using EDataLayer.Core.Repositories.Async.Abstract;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EDataLayer.Core.Repositories.Async.Concrete
{
    public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        public RepositoryAsync(DbContext context)
        {
            _context = context;
        }

        #region Get
        public async Task<TEntity> GetAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        
        public async Task<IEnumerable<TEntity>> GetAllAsync(System.Threading.CancellationToken token)
        {
            return await _context.Set<TEntity>().ToListAsync(token);
        }

        public async Task<TEntity> SingleOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync(); ;
        }
        #endregion

        #region Add
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }
        #endregion

        #region Update
        public void Update(TEntity entity)
        {
           
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry<TEntity>(entity).State = EntityState.Modified;
            }
        }
        #endregion

        #region Delete
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
        #endregion
    }
}
