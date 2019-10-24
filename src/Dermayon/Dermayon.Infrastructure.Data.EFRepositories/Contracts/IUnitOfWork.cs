using Dermayon.Common.Infrastructure.Data;
using Dermayon.Common.Infrastructure.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Infrastructure.Data.EFRepositories.Contracts
{
    /// <summary>
    /// UnitOfWork Powered by EntityFrameworkCore
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IUnitOfWork<TContext> : ITransactionAble, IDisposable
        where TContext : DbContext
    {
        /// <summary>
        /// Context 
        /// </summary>
        TContext Context { get; }
        /// <summary>
        /// Commit Transaction
        /// </summary>
        void Commit();
        /// <summary>
        /// Commit Transaction Async Mode
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();
    }
}
