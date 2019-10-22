using Dermayon.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Data
{
    public interface IRepositoryAsync<TEntity> where TEntity : EntityBase
    {
        Task InsertAsync(TEntity entitiy);
        Task InsertRangeAsync(List<TEntity> entities);
        Task UpdateAsync(TEntity entitiy);
        Task UpdateRangeAsync(List<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(List<TEntity> entities);
    }
}
