using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Infrastructure.Data.DapperRepositories.Contracts
{
    public interface IDapperRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Query(string query = null, object param = null);
        Task<IEnumerable<TEntity>> QueryAsync(string query = null, object param = null);
        void Execute(string query = null, object param = null);
        Task ExecuteAsync(string query = null, object param = null);
    }
}
