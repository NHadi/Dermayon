using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Data.Contracts
{
    public interface IDermayonUnitOfWork<TContext> : IDisposable
      where TContext : IDermayonContext
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
