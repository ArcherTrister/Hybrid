using System;

namespace Test.Web
{
    public class GenericDependencyResolver : IDependencyResolver
    {
        private IGenericObjectGraphTypeCache _cache;

        public GenericDependencyResolver(IGenericObjectGraphTypeCache cache)
        {
            _cache = cache;
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            if (!_cache.TryGetGraphType(type, out var graphType))
            {
                graphType = type
                   .GetConstructor(new Type[] { })
                   .Invoke(new object[] { });

                _cache.AddGraphType(type, graphType);
            }

            return graphType;
        }
    }
}