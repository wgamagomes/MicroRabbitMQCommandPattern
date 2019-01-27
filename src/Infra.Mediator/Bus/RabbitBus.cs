using Domain.Core.Bus;
using Domain.Core.Command;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace Infra.Mediator
{
    public class RabbitBus : IBus
    {
        private readonly IRabbitConnection _rabbitConnection;
        private readonly IModel _channel;

        public RabbitBus(IRabbitConnection rabbitConnection)
        {
            _rabbitConnection = rabbitConnection;
            _channel = _rabbitConnection.GetChannel().GetAwaiter().GetResult();
        }
        public Task Send(ICommand command)
        {
            return Task.Run(() =>
            {
                CreateExchangeIfNotExists(RabbitConfiguration.ExchangeName);

                var json = JsonConvert.SerializeObject(command, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                _channel.BasicPublish(exchange: RabbitConfiguration.ExchangeName, routingKey: RabbitConfiguration.RoutingKey, basicProperties: null, body: json.Serialize());

            });
        }

        private void CreateExchangeIfNotExists(string name) => _channel.ExchangeDeclare(exchange: name, type: ExchangeType.Fanout, durable: false, autoDelete: false, arguments: null);

        private void CreateQueueIfNotExists(string name) => _channel.QueueDeclare(name, false, false, true, null);
    }
}
