using Dermayon.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Data.Contracts
{
    /// <summary>
    /// Generic Repository that Implemented in All Repositories type Async Mode
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryAsync<TEntity> where TEntity : class
    {
        /// <summary>
        /// Insert single data Async Mode
        /// </summary>
        /// <param name="entitiy"></param>
        /// <returns></returns>
        Task InsertAsync(TEntity entitiy);
        /// <summary>
        /// Insert multiple data Async Mode
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task InsertRangeAsync(List<TEntity> entities);
        /// <summary>
        /// Update single data Async Mode
        /// </summary>
        /// <param name="entitiy"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entitiy);
        /// <summary>
        /// Update Multiple data Async Mode
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateRangeAsync(List<TEntity> entities);
        /// <summary>
        /// Delete Single Data Async Mode
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// Delete Multiple Data Async Mode
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task DeleteRangeAsync(List<TEntity> entities);
    }
}
