using Dermayon.Common.CrossCutting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.EventMessaging.Kafka
{
    public interface IServiceEventHandler
    {
        Task Handle(JObject jObject, ILog log, CancellationToken cancellationToken);
    }
}
