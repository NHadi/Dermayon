using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dermayon.Sample.SocialMedia.User.Framework.DAL
{
    [Serializable]
    public class AccountUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }        
    }
}
