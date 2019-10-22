using Dermayon.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data.Repositories.MongoDb
{
    public interface IMongoDbRepository<TEntity> : IRepository<TEntity>, IReadOnlyRepository<TEntity>
        where TEntity : EntityBase
    {
    }
}
