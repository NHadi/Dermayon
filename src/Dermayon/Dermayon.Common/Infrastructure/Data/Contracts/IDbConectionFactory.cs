using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data.Contracts
{
    /// <summary>
    /// Provide Conection factory from AppSetting->ConnectionStrings
    /// </summary>
    public interface IDbConectionFactory
    {        
        /// <summary>
        /// Get ConnectionString with Name with return of String
        /// </summary>
        /// <param name="connectionDb"></param>
        /// <returns></returns>
        string GetConnectionString(string connectionDb);
        /// <summary>
        /// Get ConnectionString with Name with return of DbConnection
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IDbConnection GetDbConnection(string connectionString);
    }
}
