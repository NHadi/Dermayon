using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data
{
    public interface IDbConectionFactory
    {        
        string GetConnectionString(string connectionDb);
        IDbConnection GetDbConnection(string connectionString);
    }
}
