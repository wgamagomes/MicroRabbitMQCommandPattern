using System;

namespace Infra.CrossCutting.IoC
{
    public interface IIoc
    {
        void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;


        void Register<TService>(Func<TService> instanceCreator)
            where TService : class;

        TImplementation GetInstance<TImplementation>() where TImplementation : class;

        void Verify();

        void ScopeWrapper(Action executeWithinScope);

        IServiceProvider GetContainer { get; }
    }
}
