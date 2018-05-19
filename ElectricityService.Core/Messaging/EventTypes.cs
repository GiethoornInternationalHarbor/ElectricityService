using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityService.Core.Messaging
{
    public enum EventTypes
    {
        Unknown,
        ServiceRequested,
        ServiceCompleted,
        ShipDocked,
        ShipUndocked
    }
}
