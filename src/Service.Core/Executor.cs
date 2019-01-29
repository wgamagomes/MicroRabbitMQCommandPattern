using Domain.Core;
using Domain.Core.Handler;
using Infra.CrossCutting.IoC;
using System;
using System.Collections.Generic;
using Topshelf;

namespace Service.Core
{
    public static class Executor
    {
        public static void Execute<TRoutine>(IIoc ioc)
           where TRoutine : class, IRoutine
        {
            HostFactory.Run(config =>
            {
                var routine = (IRoutine)ioc.GetInstance<TRoutine>();

                config.Service<RecurrenceIntervalControl>(s =>
                {
                    s.ConstructUsing(name => new RecurrenceIntervalControl(10000));
                    s.WhenStarted((serviceManager, hostControl) =>
                    {
                        serviceManager.Execute(() =>
                        {
                            routine.Execute();
                        });

                        return serviceManager.Start(hostControl);

                    });
                    s.WhenStopped((serviceManager, hostControl) => serviceManager.Stop(hostControl));
                });

                config.RunAsLocalSystem();
                config.SetServiceName("Stuff");
                config.SetDisplayName("Stuff");
                config.SetDescription("Sample Topshelf Host");
            });
        }

        public static void Listen<TEvent>(IIoc ioc)
           where TEvent : Event
        {
            Func<IEnumerable<IEventHandler<TEvent>>> eventHandlerFactory = () =>
            {
                return ioc.GetAllInstances<IEventHandler<TEvent>>();
            };

            var service = ioc.GetInstance<Listener>();

            service.Subscribe(eventHandlerFactory);
        }
    }
}
