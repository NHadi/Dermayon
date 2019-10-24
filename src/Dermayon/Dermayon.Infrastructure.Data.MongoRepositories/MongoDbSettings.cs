using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Infrastructure.Data.MongoRepositories
{
    public abstract class MongoDbSettings
    {
        public virtual string ServerConnection { get; set; }
        public virtual string Database { get; set; }
    }
}
