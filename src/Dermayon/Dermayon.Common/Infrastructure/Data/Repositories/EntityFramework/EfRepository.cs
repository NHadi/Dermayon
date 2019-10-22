using Dermayon.Common.Domain;
using Dermayon.Common.Infrastructure.Data.Repositories.EntityFramework.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Data.Repositories.EntityFramework
{
    public class EfRepository<TEntity> : IEfRepository<TEntity> where TEntity : EntityBase
    {
        private readonly IUnitOfWork<DbContext> _unitOfWork;
        private readonly DbSet<TEntity> _dbSet = null;

        public EfRepository(DbContext context)
        {
            _unitOfWork = new UnitOfWork<DbContext>(context);
            _dbSet = _unitOfWork.Context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get()
            => _dbSet.AsEnumerable();

        public virtual TEntity GetById(object id)
            => _dbSet.Find(id);
        public virtual void Insert(TEntity entitiy)
        {
            _dbSet.Attach(entitiy);
            _unitOfWork.Context.Entry(entitiy).State = EntityState.Added;
        }
        public virtual void InsertRange(List<TEntity> entitiy)
        {
            foreach (var item in entitiy)
            {
                _dbSet.Attach(item);
                _unitOfWork.Context.Entry(item).State = EntityState.Added;
            }
        }
        public virtual void Update(TEntity entitiy)
        {
            _dbSet.Attach(entitiy);
            _unitOfWork.Context.Entry(entitiy).State = EntityState.Modified;
        }
        public virtual void UpdateRange(List<TEntity> entitiy)
        {
            foreach (var item in entitiy)
            {
                _dbSet.Attach(item);
                _unitOfWork.Context.Entry(item).State = EntityState.Modified;
            }
        }
        public virtual void Delete(TEntity entitiy)
            => _dbSet.Remove(entitiy);
        public virtual void DeleteRange(List<TEntity> entities)
            => _dbSet.RemoveRange(entities);

        public virtual IEnumerable<TEntity> GetInclude(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool withTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes(query);
            }

            query = query.Where(predicate);

            if (withTracking == false)
            {
                query = query.Where(predicate).AsNoTracking();
            }

            return query.AsEnumerable();

        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool withTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            query = query.Where(predicate);
            if (withTracking == false)
            {
                query = query.Where(predicate).AsNoTracking();
            }

            return query.AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes(query);
            }

            return query.AsEnumerable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        => await _dbSet.ToListAsync();

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool withTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            query = query.Where(predicate);
            if (withTracking == false)
            {
                query = query.Where(predicate).AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetIncludeAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes(query);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetIncludeAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool withTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes(query);
            }

            query = query.Where(predicate);

            if (withTracking == false)
            {
                query = query.Where(predicate).AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
            => await _dbSet.FindAsync(id);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
