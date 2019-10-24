using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Infrastructure.Data.MongoRepositories.Contracts
{
    /// <summary>
    /// MongoContext
    /// </summary>
    public interface IMongoContext : IDisposable
    {
        /// <summary>
        /// Add Commend, Every command will be stored and it'll be processed at SaveChanges
        /// </summary>
        /// <param name="func"></param>
        void AddCommand(Func<Task> func);
        /// <summary>
        /// SaveChanges the State
        /// </summary>
        /// <returns>More than one is Success</returns>
        Task<int> SaveChanges();
        /// <summary>
        /// Get Collection in MongoDb 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
