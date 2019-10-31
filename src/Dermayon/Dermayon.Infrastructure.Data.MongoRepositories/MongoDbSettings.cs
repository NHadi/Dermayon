using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Infrastructure.Data.MongoRepositories
{    
    public class MongoDbSettings
    {
        public virtual string ServerConnection { get; set; }
        public virtual string Database{ get; set; }        
    }

    public class MongoCredentialDbSettings
    {
        public virtual string ServerConnection { get; set; }
        public virtual string Database { get; set; }
        public virtual MongoClientSettings MongoClientSettings { get; set; }
    }


}
