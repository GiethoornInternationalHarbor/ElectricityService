using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityService.Core.Messaging
{
    public interface IEventPublisher
    {
        Task HandleEventAsync<T>(EventTypes eventType, T message);
    }
}
