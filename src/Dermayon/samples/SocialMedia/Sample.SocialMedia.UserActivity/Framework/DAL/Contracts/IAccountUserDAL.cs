using Dermayon.Infrastructure.Data.MongoRepositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.SocialMedia.User.Framework.DAL.Contracts
{
    public interface IAccountUserDAL: IMongoDbRepository<UserContext,AccountUser>
    {
    }
}
