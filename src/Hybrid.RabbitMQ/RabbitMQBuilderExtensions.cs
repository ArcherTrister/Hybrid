using Hybrid;
using Hybrid.EventBus.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// builder extensions for <see cref="IHybridBuilder" />
    /// </summary>
    public static class RabbitMQBuilderExtensions
    {
        public static IApplicationBuilder UseRabbitMQ(this IHybridBuilder builder, Action<IEventBus> subscribeOption)
        {
            IApplicationBuilder app = builder.Builder;

            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            subscribeOption?.Invoke(eventBus);

            eventBus.StartSubscribe();

            return app;
            //if (app == null)
            //{
            //    throw new ArgumentNullException(nameof(app));
            //}

            //IServiceProvider provider = app.ApplicationServices;

            //var functionHandler = provider.GetServices<IHandlerTypeFinder>().ToList().Where(p => p.GetType() == typeof(IIntegrationEventHandler)).ToList();

            //var eventBus = provider.GetRequiredService<IEventBus>();

            //// todo: 向服务窗口注册所有IHandler类型
            //var services = provider.GetService<IServiceCollection>();
            //var handlerTypeFinder =
            //        services.GetOrAddTypeFinder<HandlerTypeFinder>(assemblyFinder => new HandlerTypeFinder(assemblyFinder));
            ////向服务窗口注册所有IHandler类型
            //Type[] handlerTypes = handlerTypeFinder.FindAll();
            //foreach (var handlerType in handlerTypes)
            //{
            //    Type handlerInterface = handlerType.GetInterface("IIntegrationEventHandler`1"); //获取该类实现的泛型接口
            //    if (handlerInterface == null)
            //    {
            //        continue;
            //    }
            //    Type eventDataType = handlerInterface.GetGenericArguments()[0]; //泛型的EventData类型
            //    var iEvent = handlerType.GetInterfaces().Where(x => x.IsGenericType).FirstOrDefault()?.GetGenericArguments().FirstOrDefault();
            //    if (iEvent != null)
            //    {
            //        eventBus.Subscribe < typeof(eventDataType), IIntegrationEventHandler <>> ();
            //    }
            //}
        }
    }
}