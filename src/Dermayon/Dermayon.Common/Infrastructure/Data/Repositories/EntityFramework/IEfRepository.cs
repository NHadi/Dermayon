using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data.Repositories.EntityFramework
{
    public interface IEfRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entitiy);
        void InsertRange(List<TEntity> entities);
        void Update(TEntity entitiy);
        void UpdateRange(List<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(List<TEntity> entities);
        //void Save();
        //Task SaveAsync();
    }
}
