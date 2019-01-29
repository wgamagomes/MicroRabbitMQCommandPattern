using Domain.Core;
using Domain.Core.Bus;
using Domain.Core.Handler;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Infra.Mediator
{
    public class RabbitEventBus : IEventBus
    {
        private readonly IRabbitConnection _rabbitConnection;
        private readonly IModel _channel;

        public RabbitEventBus(IRabbitConnection rabbitConnection)
        {
            _rabbitConnection = rabbitConnection;
            _channel = _rabbitConnection.GetChannel().GetAwaiter().GetResult();
        }
        public Task Publish(Event @event)
        {
            return Task.Run(() =>
            {
                var eventType = @event.GetType();
                var exchange = Exchange(eventType);

                ExchangeDeclare(exchange);

                var json = JsonConvert.SerializeObject(@event, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                _channel.BasicPublish(exchange: Exchange(eventType), routingKey: RoutingKey(eventType), basicProperties: null, body: json.Serialize());

            });
        }

        public Task Subscribe<TEvent>(Func<IEnumerable<IEventHandler<TEvent>>> eventHandlerFactory)
            where TEvent : Event
        {
            var eventType = typeof(TEvent);
            var routingKey = RoutingKey(eventType);
            var exchange = Exchange(eventType);
            var queue = Queue(eventType);

            QueueDeclare(queue);
            QueueBind(queue, exchange, routingKey);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, e) =>
            {
                var evento = JsonConvert.DeserializeObject<TEvent>(e.Body.Deserialize<string>());

                foreach (var handler in eventHandlerFactory.Invoke())
                {
                    HandleEvent(handler, evento);
                }
            };

            _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }


        private Task HandleEvent<TEvent>(IEventHandler<TEvent> subscription, TEvent @event)
             where TEvent : Event
        {
            return Task.Run(() =>
            {
                subscription.Handler(@event);
            });
        }

        public string Exchange(Type @event)
        {
            return $"{@event.Name}-exchange";
        }

        public string Queue(Type @event)
        {
            return $"{@event.Name}-queue";
        }

        public string RoutingKey(Type @event)
        {
            return $"{@event.Name}-routing-Key";
        }

        private void ExchangeDeclare(string exchange) => _channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout, durable: false, autoDelete: false, arguments: null);

        private void QueueDeclare(string queue, bool durable = false, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null) => _channel.QueueDeclare(queue, durable, exclusive, autoDelete, arguments);

        private void QueueBind(string queue, string exchange, string routingKey, IDictionary<string, object> arguments = null) => _channel.QueueBind(queue: queue, exchange: exchange, routingKey: routingKey, arguments: arguments);

    }
}
