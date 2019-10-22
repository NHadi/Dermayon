using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Data.Repositories.EntityFramework.UoW
{
    public interface IUnitOfWork<TContext> : ITransactionAble, IDisposable
        where TContext : DbContext
    {
        TContext Context { get; }
        void Commit();
        Task CommitAsync();
    }
}
