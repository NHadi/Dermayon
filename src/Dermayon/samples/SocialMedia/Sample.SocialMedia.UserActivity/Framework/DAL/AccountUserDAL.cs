using Dermayon.Infrastructure.Data.MongoRepositories;
using Sample.SocialMedia.User.Framework.DAL.Contracts;

namespace Sample.SocialMedia.User.Framework.DAL
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
