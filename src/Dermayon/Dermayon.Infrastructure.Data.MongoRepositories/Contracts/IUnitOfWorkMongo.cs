using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Infrastructure.Data.MongoRepositories.Contracts
{
    /// <summary>
    /// UnitOfWork Powered by MongoDb
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IUnitOfWorkMongo<TContext> : IDisposable
       where TContext : MongoContext
    {
        /// <summary>
        /// Context of MongoDb
        /// </summary>
        TContext Context { get; }
        /// <summary>
        /// Commit Transaction
        /// </summary>
        /// <returns>True / False</returns>
        Task<bool> Commit();
    }
}
