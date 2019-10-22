using Dermayon.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        void Insert(TEntity entitiy);
        void InsertRange(List<TEntity> entities);
        void Update(TEntity entitiy);
        void UpdateRange(List<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(List<TEntity> entities);
    }
}
