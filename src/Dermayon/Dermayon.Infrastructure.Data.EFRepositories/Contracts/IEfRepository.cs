using Dermayon.Common.Infrastructure.Data.Contracts;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dermayon.Infrastructure.Data.EFRepositories.Contracts
{
    /// <summary>
    /// Generic Repository Powered by EntityFrameworkCore 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEfRepository<TEntity> : IRepository<TEntity>, IReadOnlyRepository<TEntity>, IDisposable
       where TEntity : class
    {
        /// <summary>
        /// Get Include with Linq 
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);
        /// <summary>
        /// Get Include with filter predicate
        /// </summary>
        /// <param name="predicate">filter with predicate</param>
        /// <param name="includes">include by linq</param>
        /// <param name="withTracking">AsNoTracking / Tracking by Ef</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetInclude(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool withTracking = true);
        /// <summary>
        /// Get Include with Linq Async Mode
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetIncludeAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);
        /// <summary>
        /// Get Include with filter predicate Async Mode
        /// </summary>
        /// <param name="predicate">filter with predicate</param>
        /// <param name="includes">include by linq</param>
        /// <param name="withTracking">AsNoTracking / Tracking by Ef</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetIncludeAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool withTracking = true);
    }
}
