using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.Events
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EventAttribute : Attribute
    {
        public string Name { get; }

        public EventAttribute(string name)
        {
            Name = name;
        }
    }
}
