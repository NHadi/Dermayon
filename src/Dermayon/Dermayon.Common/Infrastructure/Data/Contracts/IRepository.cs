using Dermayon.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data.Contracts
{
    /// <summary>
    /// Generic Repository that Implemented in All Repositories type
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Insert single data
        /// </summary>
        /// <param name="entitiy"></param>
        void Insert(TEntity entitiy);
        /// <summary>
        /// Insert multiple data 
        /// </summary>
        /// <param name="entities"></param>
        void InsertRange(List<TEntity> entities);
        /// <summary>
        /// Update single data
        /// </summary>
        /// <param name="entitiy"></param>
        void Update(TEntity entitiy);
        /// <summary>
        /// Update Multiple data
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(List<TEntity> entities);
        /// <summary>
        /// Delete Single Data
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// Delete Multiple Data
        /// </summary>
        /// <param name="entities"></param>
        void DeleteRange(List<TEntity> entities);
    }
}
