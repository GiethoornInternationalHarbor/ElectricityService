using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ElectricityService.Core;
using ElectricityService.Core.Messaging;
using ElectricityService.Core.Services;
using ElectricityService.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityService.Infrastructure.DI
{
	public static class DIHelper
    {   
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IElectricityService, Services.ElectricityService>();

            services.AddSingleton<IEventHandler, RMQMessageHandler>((provider) => new RMQMessageHandler(configuration.GetSection("AMQP").Value));
            services.AddTransient<IEventPublisher, RMQMessagePublisher>((provider) => new RMQMessagePublisher(configuration.GetSection("AMQP").Value));
        }
      
    }
}
