using System;

namespace Hybrid.Dependency
{
    public interface ISingletonFactory
    {
        TService GetService<TService>(string id) where TService : class;

        TService GetService<TService>(Type serviceType, string id) where TService : class;

        void AddService<TService>(TService service, string id) where TService : class;

        void AddService(Type serviceType, object service, string id);
    }
}