using Infra.CrossCutting.IoC;
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
                    s.ConstructUsing(name => new RecurrenceIntervalControl(60000));
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
    }
}
