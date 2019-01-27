using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Web.Http.Dependencies;

namespace Infra.CrossCutting.IoC
{
    public class DependencyInjector : IIoc
    {
        private readonly Container _container;
        private readonly ObjectLifetime _objectLifetime;
        private readonly ScopedLifetime? _scopedLifetime;
        private bool _isConfigured;

        public IServiceProvider GetContainer { get { return _container; } }

        public DependencyInjector()
            : this(ObjectLifetime.Transient)
        {
        }

        public DependencyInjector(ObjectLifetime lifestyle)
        {
            _container = new Container();
            _objectLifetime = lifestyle;
        }

        public DependencyInjector(ScopedLifetime scopedLifetime)
            : this(ObjectLifetime.Scoped)
        {
            _scopedLifetime = scopedLifetime;
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            if (!_isConfigured)
                ConfigureContainer();

            _container.Register<TService, TImplementation>();
        }

        public void Register<TService>(Func<TService> instanceCreator) where TService : class
        {
            if (!_isConfigured)
                ConfigureContainer();

            _container.Register(instanceCreator, GetLifestyle(_objectLifetime));
        }

        public void Verify()
        {
            _container.Verify();
        }

        private void ConfigureContainer()
        {
            _isConfigured = true;

            if (_container == null)
                throw new InvalidOperationException("Unable to resolve dependencies before the container has been initialized.");

            if (_objectLifetime == ObjectLifetime.Scoped)
                if (_scopedLifetime == null)
                    throw new ArgumentNullException($"The ScopedLifestyle can't be nul.");
                else
                    _container.Options.DefaultScopedLifestyle = GetScopedLifestyle((ScopedLifetime)_scopedLifetime);
        }

        public IDependencyResolver GetResolver()
        {
            return new SimpleInjector.Integration.WebApi.SimpleInjectorWebApiDependencyResolver(_container);
        }

        public void ScopeWrapper(Action executeWithinScope)
        {
            switch (_scopedLifetime)
            {
                case ScopedLifetime.ThreadScoped:
                    using (ThreadScopedLifestyle.BeginScope(_container))
                        executeWithinScope();
                    break;

                case ScopedLifetime.AsyncScoped:
                    using (AsyncScopedLifestyle.BeginScope(_container))
                        executeWithinScope();
                    break;
            }
        }

        public TImplementation GetInstance<TImplementation>() where TImplementation : class
        {
            if (_container == null) throw new InvalidOperationException("Unable to resolve dependencies before the container has been initialized.");
            return _container.GetInstance<TImplementation>();
        }

        private Lifestyle GetLifestyle(ObjectLifetime lifestyle)
        {
            switch (lifestyle)
            {
                case ObjectLifetime.Transient:
                    return Lifestyle.Transient;
                case ObjectLifetime.Singleton:
                    return Lifestyle.Singleton;
                default:
                    return Lifestyle.Scoped;
            }
        }

        private ScopedLifestyle GetScopedLifestyle(ScopedLifetime scopedLifetime)
        {
            switch (scopedLifetime)
            {
                //case ScopedLifetime.WebApiRequest:
                //    return new WebRequestLifestyle();
                case ScopedLifetime.ThreadScoped:
                    return new ThreadScopedLifestyle();
                case ScopedLifetime.AsyncScoped:
                    return new AsyncScopedLifestyle();
                default:
                    return null;
            }
        }
    }
    public enum ObjectLifetime
    {
        Transient = 0,
        Singleton = 1,
        Scoped = 2

    }

    public enum ScopedLifetime
    {
        ThreadScoped = 0,
        AsyncScoped = 1,
        WebApiRequest = 2,
        WCFOperation = 3
    }
}
