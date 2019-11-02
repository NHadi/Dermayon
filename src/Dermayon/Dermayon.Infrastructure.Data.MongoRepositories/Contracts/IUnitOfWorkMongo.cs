using Dermayon.Common.Infrastructure.Data.Contracts;
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
    public interface IUnitOfWorkMongo<TContext> : IDermayonUnitOfWork<TContext>, IDisposable
       where TContext : MongoContext
    {
        
    }
}
