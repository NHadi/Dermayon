using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Infrastructure.Data.MongoRepositories
{
    public class Credential
    {
        public virtual string Db { get; set; }
        public virtual string User { get; set; }
        public virtual string Password { get; set; }
    }
    public class MongoDbSettings
    {
        public virtual string ServerConnection { get; set; }
        public virtual string Database{ get; set; }
        public virtual Credential Credential { get; set; }
    }
}
