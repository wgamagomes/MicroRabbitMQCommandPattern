using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Infra.Mediator
{
    public class RabbitConnection : IRabbitConnection
    {
        private IConnection _connection;

        public async Task<IModel> GetChannel()
        {
            if (_connection == null)
                await Open();

            return _connection.CreateModel();
        }

        public Task<bool> Open()
        {
            try
            {
                if (_connection?.IsOpen == null || _connection?.IsOpen == false)
                {
                    var rabbitSection = RabbitSection.Section.Settings;

                    var rabbitFactory = new ConnectionFactory
                    {
                        HostName = rabbitSection.Host,
                        UserName = rabbitSection.User,
                        Password = rabbitSection.Password
                    };

                    Policy.Handle<BrokerUnreachableException>()
                          .WaitAndRetry(3, x => TimeSpan.FromSeconds(10))
                          .Execute(() => _connection = rabbitFactory.CreateConnection());
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error trying to open connection to RabbitMQ. {ex.Message}");
            }
        }
    }
}
