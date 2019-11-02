using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Dermayon.Common.Events
{
    public class Event : IEvent
    {
        
        public Guid Id
        {
            get
            {
                return Guid.NewGuid();
            }
        }
        public DateTime Created 
        {
            get
            {
                return DateTime.Now;
            }
        }
        public string EventName
        {
            get
            {
                return "Test";
            }
        }

    }
}
