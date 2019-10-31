using Dermayon.Infrastructure.Data.MongoRepositories;
using Dermayon.Sample.SocialMedia.User.Framework.DAL.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dermayon.Sample.SocialMedia.User.Framework.DAL
{
    public class AccountUserDAL : MongoDbRepository<UserContext, AccountUser>, IAccountUserDAL
    {
        private readonly UserContext _context;
        public AccountUserDAL(UserContext context) : base(context)
        {
            _context = context;
        }        
    }
}
