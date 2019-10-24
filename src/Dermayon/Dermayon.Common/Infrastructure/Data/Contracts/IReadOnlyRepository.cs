using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Data.Contracts
{
    /// <summary>
    /// Provide Generic read operation in repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Get();
        /// <summary>
        /// Get data with Filter by predicate and can set tracking the state or not
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="withTracking"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool withTracking = true);        
        /// <summary>
        /// Get data by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(object id);
        /// <summary>
        /// Get All Data asyn mode
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync();
        /// <summary>
        /// Get data with Filter by predicate and can set tracking the state or not in Async Mdoe
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="withTracking"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool withTracking = true);        
        /// <summary>
        /// Get data by Id Async Mode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(object id);
    }
}
