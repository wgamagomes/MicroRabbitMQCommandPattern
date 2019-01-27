using RabbitMQ.Client;
using System.Threading.Tasks;

namespace Infra.CrossCutting.Bus
{
    public interface IRabbitConnection
    {
        Task<bool> Open();

        Task<IModel> GetChannel();
    }
}