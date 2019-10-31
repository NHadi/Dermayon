using Dermayon.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Data.Contracts
{
    /// <summary>
    /// Generic Repository that Implemented in All Repositories type Async Mode
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEventRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Insert single data Async Mode
        /// </summary>
        /// <param name="entitiy"></param>
        /// <returns></returns>
        Task InsertEvent(TEntity entitiy, CancellationToken cancellationToken);
        /// <summary>
        /// Insert multiple data Async Mode
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task InsertRangeEvent(List<TEntity> entities, CancellationToken cancellationToken);
        /// <summary>
        /// Update single data Async Mode
        /// </summary>
        /// <param name="entitiy"></param>
        /// <returns></returns>
        Task UpdateEvent(TEntity entitiy, CancellationToken cancellationToken);
        /// <summary>
        /// Update Multiple data Async Mode
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateRangeEvent(List<TEntity> entities, CancellationToken cancellationToken);
        /// <summary>
        /// Delete Single Data Async Mode
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteEvent(TEntity entity, CancellationToken cancellationToken);
        /// <summary>
        /// Delete Multiple Data Async Mode
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task DeleteRangeEvent(List<TEntity> entities, CancellationToken cancellationToken);
    }
}
