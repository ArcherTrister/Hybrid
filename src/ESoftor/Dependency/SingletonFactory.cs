using System;
using System.Collections.Generic;

namespace ESoftor.Dependency
{
    public class SingletonFactory : ISingletonFactory
    {
        //SingletonFactory singletonFactory = new SingletonFactory();
        //singletonFactory.AddService<LocalizationDictionaryProviderBase>(new XmlEmbeddedFileLocalizationDictionaryProvider(), "impla1");
        //singletonFactory.AddService<LocalizationDictionaryProviderBase>(new JsonEmbeddedFileLocalizationDictionaryProvider(), "impla2");
        //singletonFactory.AddService<LocalizationDictionaryProviderBase>(new XmlFileLocalizationDictionaryProvider(), "impla3");
        //singletonFactory.AddService<LocalizationDictionaryProviderBase>(new JsonFileLocalizationDictionaryProvider(), "impla4");
        //services.AddSingleton(singletonFactory);
        //public XXXX(SingletonFactory singletonFactory)
        //{
        //    this.serviceA = singletonFactory.GetService<IServiceA>("impla2"); //使用标识从SingletonFactory获取自己想要的服务实现
        //}

        private readonly Dictionary<Type, Dictionary<string, object>> _serviceDict;

        public SingletonFactory()
        {
            _serviceDict = new Dictionary<Type, Dictionary<string, object>>();
        }

        public TService GetService<TService>(string id) where TService : class
        {
            Type serviceType = typeof(TService);
            return GetService<TService>(serviceType, id);
        }

        public TService GetService<TService>(Type serviceType, string id) where TService : class
        {
            if (!_serviceDict.TryGetValue(serviceType, out Dictionary<string, object> implDict)) return null;
            if (implDict.TryGetValue(id, out object service))
            {
                return service as TService;
            }
            return null;
        }

        public void AddService<TService>(TService service, string id) where TService : class
        {
            AddService(typeof(TService), service, id);
        }

        public void AddService(Type serviceType, object service, string id)
        {
            if (service == null) return;
            if (_serviceDict.TryGetValue(serviceType, out Dictionary<string, object> implDict))
            {
                implDict[id] = service;
            }
            else
            {
                implDict = new Dictionary<string, object> { [id] = service };
                _serviceDict[serviceType] = implDict;
            }
        }
    }
}
