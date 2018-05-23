using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityService.Core.Messaging
{
    public interface IEventHandler
    {
        void Start(IEventHandlerCallback callback);
        void Stop();
    }
}
