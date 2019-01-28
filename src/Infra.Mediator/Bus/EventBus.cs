using Domain.Core.Bus;
using Domain.Core;
using Domain.Core.Handler;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace Infra.Mediator
{
    public class EventBus : IEventBus
    {
        private readonly IRabbitConnection _rabbitConnection;
        private readonly IModel _channel;

        public EventBus(IRabbitConnection rabbitConnection)
        {
            _rabbitConnection = rabbitConnection;
            _channel = _rabbitConnection.GetChannel().GetAwaiter().GetResult();
        }
        public Task Publish(Event @event)
        {
            return Task.Run(() =>
            {
                CreateExchangeIfNotExists(RabbitConfiguration.ExchangeName);

                var json = JsonConvert.SerializeObject(@event, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                _channel.BasicPublish(exchange: RabbitConfiguration.ExchangeName, routingKey: RabbitConfiguration.RoutingKey, basicProperties: null, body: json.Serialize());

            });
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            throw new System.NotImplementedException();
        }

        private void CreateExchangeIfNotExists(string name) => _channel.ExchangeDeclare(exchange: name, type: ExchangeType.Fanout, durable: false, autoDelete: false, arguments: null);

        private void CreateQueueIfNotExists(string name) => _channel.QueueDeclare(name, false, false, true, null);
    }
}
