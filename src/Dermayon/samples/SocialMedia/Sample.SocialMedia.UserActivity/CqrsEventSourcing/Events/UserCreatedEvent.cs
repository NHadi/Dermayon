using Dermayon.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.SocialMedia.User.CqrsEventSourcing.Events
{
    [Event("UserCreated")]
    public class UserCreatedEvent : Event
    {        
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
