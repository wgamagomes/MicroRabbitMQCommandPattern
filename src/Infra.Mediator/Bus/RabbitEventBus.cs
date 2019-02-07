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
                var exchangeName = ExchangeName(eventType);
                var properties = _channel.CreateBasicProperties();
                properties.DeliveryMode = 2;

                ExchangeDeclare(exchangeName);

                var json = JsonConvert.SerializeObject(@event, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                _channel.BasicPublish(exchange: exchangeName, routingKey: RoutingKey(eventType), basicProperties: properties, body: json.Serialize());

            });
        }

        public Task Subscribe<TEvent>(Func<IEnumerable<IEventHandler<TEvent>>> eventHandlerFactory)
            where TEvent : Event
        {
            var eventType = typeof(TEvent);
            var routingKey = RoutingKey(eventType);
            var exchangeName = ExchangeName(eventType);

            foreach (var handler in eventHandlerFactory.Invoke())
            {
                var handlerType = handler.GetType();
                var queueName = QueueName(handlerType);
                QueueDeclare(queueName);
                QueueBind(queueName, exchangeName, routingKey);

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (sender, e) =>
                {
                    try
                    {
                        var @event = JsonConvert.DeserializeObject<TEvent>(e.Body.Deserialize<string>());

                        HandleEvent(handler, @event);

                        _channel.BasicAck(e.DeliveryTag, false);
                    }

                    catch (Exception ex)
                    {
                        string error = $"an error occurred while handling the event. Exchange: {exchangeName}, Queue : {queueName}, Message: {ex.Message}";
                        _channel.BasicNack(e.DeliveryTag, false, !e.Redelivered);
                    }
                };

                _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            }
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

        public string ExchangeName(Type @event)
        {
            return $"{@event.Name}-exchange";
        }

        public string QueueName(Type @event)
        {
            return $"{@event.Name}-queue";
        }

        public string RoutingKey(Type @event)
        {
            return $"{@event.Name}-routing-Key";
        }

        private void ExchangeDeclare(string exchange) => _channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout, durable: false, autoDelete: false, arguments: null);

        private void QueueDeclare(string queue, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null) => _channel.QueueDeclare(queue, durable, exclusive, autoDelete, arguments);

        private void QueueBind(string queue, string exchange, string routingKey, IDictionary<string, object> arguments = null) => _channel.QueueBind(queue: queue, exchange: exchange, routingKey: routingKey, arguments: arguments);

    }
}
