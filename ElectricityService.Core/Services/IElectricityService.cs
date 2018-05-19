using ElectricityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityService.Core.Services
{
    public interface IElectricityService
    {
        #region --send events---
        Task SendServiceCompletedAsync(Ship ship);//send event that this service is completed to ship
        #endregion
    }
}
