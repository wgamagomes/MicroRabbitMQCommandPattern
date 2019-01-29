using Domain.Core.Bus;
using Infra.Mediator;
using Infra.CrossCutting.IoC;
using Service.Core;
using Domain.Handler;
using Domain.Core.Handler;
using Domain.Core;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;

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
            
            // Executor.Execute<DoSomethingService>(ioc);
           
                       
            
            Executor.Listen<StuffEvent>(ioc);
        }
    }
}
