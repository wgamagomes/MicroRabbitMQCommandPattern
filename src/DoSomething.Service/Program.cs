using Domain;
using Domain.Core.Bus;
using Domain.Core.Handler;
using Domain.Handler;
using Infra.CrossCutting.IoC;
using Infra.Mediator;
using Service.Core;
using System.Threading.Tasks;

namespace DoSomething.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioc = new DependencyInjector(ScopedLifetime.AsyncScoped);

            ioc.Register<IEventBus, RabbitEventBus>();
            ioc.Register<IRabbitConnection, RabbitConnection>();
            ioc.RegisterCollection<IEventHandler<StuffEvent>>(typeof(StuffEventHandler), typeof(WhateverEventHandler));

            Task.Run(() => {
                /*Producer service test*/
                Executor.Execute<DoSomethingService>(ioc);
            });

            /*Event handlers test*/
            Executor.Listen<StuffEvent>(ioc);
        }
    }
}
