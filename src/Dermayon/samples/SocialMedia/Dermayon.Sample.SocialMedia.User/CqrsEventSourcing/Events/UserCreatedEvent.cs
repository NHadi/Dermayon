using Dermayon.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dermayon.Sample.SocialMedia.User.CqrsEventSourcing.Events
{
    [Event("UserCreated")]
    public class UserCreatedEvent : IEvent
    {
        public Guid? Parent { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
