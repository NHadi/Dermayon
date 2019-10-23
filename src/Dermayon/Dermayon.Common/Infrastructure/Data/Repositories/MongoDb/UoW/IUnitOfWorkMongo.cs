using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Data.Repositories.MongoDb.UoW
{
    public interface IUnitOfWorkMongo<TContext> : IDisposable
        where TContext : MongoContext
    {
        TContext Context { get; }
        Task<bool> Commit();
    }
}
