using Dermayon.Common.CrossCutting;
using Dermayon.Infrastructure.Data.MongoRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dermayon.Sample.SocialMedia.User.Framework.DAL
{
    public class UserContext : MongoContext
    {

        public UserContext(UserContextSetting settings) : base(settings)
        {

        }
    }
}
