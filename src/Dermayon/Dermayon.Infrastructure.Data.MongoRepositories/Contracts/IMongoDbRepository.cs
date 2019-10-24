using Dermayon.Common.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Infrastructure.Data.MongoRepositories.Contracts
{
    public interface IMongoDbRepository<TEntity> : IRepository<TEntity>, IReadOnlyRepository<TEntity>
         where TEntity : class
    {
    }
}
