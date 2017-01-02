using System;
using EDataLayer.Core.Repositories.Abstract;
using System.Data.Entity;
using System.Linq;

namespace EDataLayer.Core.Repositories.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
        }

        #region Get
        public TEntity Get(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public System.Collections.Generic.IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public System.Collections.Generic.IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }
        #endregion

        #region Add
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(System.Collections.Generic.IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }
        #endregion

        #region Update
        public void Update(TEntity entity)
        {
           
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void UpdateRange(System.Collections.Generic.IEnumerable<TEntity> entities)
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

        public void RemoveRange(System.Collections.Generic.IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
        #endregion
    }
}
