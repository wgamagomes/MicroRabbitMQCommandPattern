using RabbitMQ.Client;
using System.Threading.Tasks;

namespace Infra.Mediator
{
    public interface IRabbitConnection
    {
        Task<bool> Open();

        Task<IModel> GetChannel();
    }
}