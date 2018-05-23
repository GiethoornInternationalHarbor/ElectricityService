using ElectricityService.Core.Messaging;
using Polly;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utf8Json;

namespace ElectricityService.Infrastructure.Messaging
{
    public class RMQMessagePublisher : IEventPublisher
    {
        private Uri _uri;
        private string _exchange;

        public RMQMessagePublisher(string uri, string exchange = RMQMessageExchanges.Default)
        {
            _uri = new Uri(uri);
            _exchange = exchange;
        }

        public Task HandleEventAsync<T>(EventTypes eventType, T message)
        {
            return Task.Run(() =>
                Policy
                    .Handle<Exception>()
                    .WaitAndRetry(9, r => TimeSpan.FromSeconds(5), (ex, ts) => { Console.WriteLine("Error connecting to RabbitMQ. Retrying in 5 sec."); })
                    .Execute(() =>
                    {
                        var factory = new ConnectionFactory() { Uri = _uri };
                        using (var connection = factory.CreateConnection())
                        {
                            using (var model = connection.CreateModel())
                            {
                                model.ExchangeDeclare(_exchange, "fanout", durable: true, autoDelete: false);
                                var data = JsonSerializer.Serialize(message);
                                IBasicProperties properties = model.CreateBasicProperties();
                                properties.Type = eventType.ToString();
                                model.BasicPublish(_exchange, "", properties, data);
                            }
                        }
               }));
        }
    }
}
