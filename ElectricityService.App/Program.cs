using dotenv.net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ElectricityService.App.Messaging;
using ElectricityService.Core.Messaging;
using ElectricityService.Infrastructure.DI;
using System;
using System.IO;
using System.Threading;
using Utf8Json.Resolvers;

namespace ElectricityService.App
{
    class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

    static Program()
    {
        string filePath = ".env";

#if DEBUG
        filePath = Path.Combine(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.LastIndexOf("bin")), filePath);
#endif

        DotEnv.Config(throwOnError: false, filePath: filePath);

        //setup our DI
        var serviceCollection = new ServiceCollection();

        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();   
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        DIHelper.Setup(services, config);

        // Setup the app services
        services.AddTransient<IEventHandlerCallback, ElectricityEventHandler>();
    }

    static void Main(string[] args)
    {
        CompositeResolver.RegisterAndSetAsDefault(new[]
        {
                EnumResolver.UnderlyingValue,
                StandardResolver.ExcludeNullCamelCase
            });

        //bind eventhandlers
        IEventHandler eventHandler = ServiceProvider.GetService<IEventHandler>();
        IEventHandlerCallback eventHandlerCallback = ServiceProvider.GetService<IEventHandlerCallback>();

        try
        {         
            eventHandler.Start(eventHandlerCallback);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occured during eventhandler startup check logs for info"+ ex.Message);
            Environment.Exit(1);
        }

        Console.WriteLine("Up and running and ready to rumble!");
        while (true)
        {
            Thread.Sleep(10000);
        }
    }
}
}