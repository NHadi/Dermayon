using Dermayon.Common.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Infrastructure.EvenMessaging.Kafka.Contracts
{
    public interface IKakfaProducer
    {
        Task Send(IEvent @event, string topic);
    }
}
