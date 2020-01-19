using Hybrid.EventBus;
using Hybrid.EventBus.Abstractions;
using Hybrid.RabbitMQ;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 通过IOC创建单例的RabbitMQ的客户端
    /// </summary>
    public static class RabbitMQEventBusBuilderExtensions
    {
        /// <summary>
        /// 添加RabbitMQ扩展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQ(this IEventBusBuilder builder, IConfiguration configuration)
        {
            IServiceCollection services = builder.Services;
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration["RabbitMQ:Connection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["RabbitMQ:UserName"]))
                {
                    factory.UserName = configuration["RabbitMQ:UserName"];
                }

                if (!string.IsNullOrEmpty(configuration["RabbitMQ:Password"]))
                {
                    factory.Password = configuration["RabbitMQ:Password"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["RabbitMQ:RetryCount"]))
                {
                    retryCount = int.Parse(configuration["RabbitMQ:RetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            var subscriptionClientName = configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                //var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var serviceProvider = services.BuildServiceProvider();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, serviceProvider, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });
            //services.AddTransient<ProductPriceChangedIntegrationEventHandler>();

            return services;
        }
    }
}