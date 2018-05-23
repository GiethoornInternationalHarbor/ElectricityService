using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityService.Core.Messaging
{
    public interface IEventHandlerCallback
    {
        Task<bool> HandleEventAsync(EventTypes eventType, string message);
    }
}
