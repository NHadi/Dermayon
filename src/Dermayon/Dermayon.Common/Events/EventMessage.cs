using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.Events
{
    public class EventMessage
    {
        public string Name { get; set; }
        public string EventData { get; set; }
        public DateTime Date { get; set; }
    }
}
