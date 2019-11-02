using Dermayon.Common.Domain;
using Dermayon.Common.Events;
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
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventRepository<TContext, TEvent> 
        where TContext : IDermayonContext
         where TEvent : IEvent
    {
        /// <summary>
        /// Insert single data Async Mode
        /// </summary>
        /// <param name="@event"></param>
        /// <returns></returns>
        Task InserEvent(TEvent @event, CancellationToken cancellationToken);
        /// <summary>
        /// Insert multiple data Async Mode
        /// </summary>
        /// <param name="@events"></param>
        /// <returns></returns>
        Task InsertRangeEvent(List<TEvent> @events, CancellationToken cancellationToken);
        /// <summary>
        /// Update single data Async Mode
        /// </summary>
        /// <param name="@event"></param>
        /// <returns></returns>
        Task UpdateEvent(TEvent @event, CancellationToken cancellationToken);
        /// <summary>
        /// Update Multiple data Async Mode
        /// </summary>
        /// <param name="@events"></param>
        /// <returns></returns>
        Task UpdateRangeEvent(List<TEvent> @events, CancellationToken cancellationToken);
        /// <summary>
        /// Delete Single Data Async Mode
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteEvent(TEvent @event, CancellationToken cancellationToken);
        /// <summary>
        /// Delete Multiple Data Async Mode
        /// </summary>
        /// <param name="@events"></param>
        /// <returns></returns>
        Task DeleteRangeEvent(List<TEvent> @events, CancellationToken cancellationToken);
    }
}
