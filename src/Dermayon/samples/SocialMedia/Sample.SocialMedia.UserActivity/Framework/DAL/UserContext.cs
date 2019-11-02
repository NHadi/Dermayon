using Dermayon.Infrastructure.Data.MongoRepositories;

namespace Sample.SocialMedia.User.Framework.DAL
{
    public class UserContext : MongoContext
    {

        public UserContext(UserContextSetting settings) : base(settings)
        {

        }
    }
}
