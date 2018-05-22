using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ElectricityService.Infrastructure.Database;
using ElectricityService.Core.Messaging;
using ElectricityService.Core.Repositories;
using ElectricityService.Core.Services;
using ElectricityService.Infrastructure.Messaging;
using ElectricityService.Infrastructure.Repositories;
using System;

namespace ElectricityService.Infrastructure.DI
{
    public static class DIHelper
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ElectricityDbContext>(options => options.UseSqlServer(configuration.GetSection("DB_CONNECTION_STRING").Value));

            services.AddTransient<IShipRepository, ShipRepository>();
            services.AddTransient<IElectricityService, Services.ElectricityService>();

            services.AddSingleton<IEventHandler, RMQMessageHandler>((provider) => new RMQMessageHandler(configuration.GetSection("AMQP").Value));
            services.AddTransient<IEventPublisher, RMQMessagePublisher>((provider) => new RMQMessagePublisher(configuration.GetSection("AMQP").Value));

        }

        public static void OnServicesSetup(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Connecting to database and migrating if required");
            var dbContext = serviceProvider.GetService<ElectricityDbContext>();
            dbContext.Database.Migrate();
            Console.WriteLine("Completed connecting to database");
        }
    }
}
