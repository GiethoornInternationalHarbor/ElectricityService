using ElectricityService.Core.Messaging;
using ElectricityService.Core.Models;
using ElectricityService.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityService.Infrastructure.Services
{
    public class ElectricityService : IElectricityService
    {
       
        private readonly IEventPublisher _eventPublisher;

        public ElectricityService(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
           }


        public Task SendServiceCompletedAsync(Ship ship)
        {
            return Task.Run(async () =>
            {
              
                ShipService shipService = new ShipService();
                shipService.Name = "Electricity";
                shipService.Id = new Guid();
                shipService.Price = 2500;
                ship.ShipService = shipService;
                await _eventPublisher.HandleEventAsync(EventTypes.ServiceCompleted, ship);
            });
        } 
    }
}
