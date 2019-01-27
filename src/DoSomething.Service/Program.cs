using Domain.Core.Bus;
using Infra.Mediator;
using Infra.CrossCutting.IoC;
using Service.Core;

namespace DoSomething.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioc = new DependencyInjector(ScopedLifetime.AsyncScoped);

            ioc.Register<IBus, RabbitBus>();
            ioc.Register<IRabbitConnection, RabbitConnection>();

            Executor.Execute<DoSomethingService>(ioc);
        }
    }
}
