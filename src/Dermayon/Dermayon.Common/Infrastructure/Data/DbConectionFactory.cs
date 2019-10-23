using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data
{
    public class DbConectionFactory : IDbConectionFactory
    {
        private readonly IConfiguration _configuration;
        public DbConectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string connectionString) => _configuration.GetConnectionString(connectionString);
        public IDbConnection GetDbConnection(string connectionString) => new SqlConnection(_configuration.GetConnectionString(connectionString));
    }
}
