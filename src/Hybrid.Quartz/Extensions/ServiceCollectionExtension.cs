using Hybrid.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;

namespace Hybrid.Quartz.Extensions
{
    internal static class ServiceCollectionExtension
    {
        /// <summary>
        /// 注册Job
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InitializeJobs(this IServiceCollection services)
        {
            //            jobsAssembly.Assembly.GetTypes()
            //            .Where(type => !type.IsAbstract && typeof(IJob).IsAssignableFrom(type))
            //            .Each(x => container.RegisterAutoWiredType(x));
            var jobTypeFinder =
                services.GetOrAddTypeFinder<IJobTypeFinder>(assemblyFinder => new JobTypeFinder(assemblyFinder));
            //向服务窗口注册所有IJob类型
            Type[] jobTypes = jobTypeFinder.FindAll();
            foreach (Type jobType in jobTypes)
            {
                if (jobType == null)
                {
                    continue;
                }
                services.TryAddTransient(jobType);
            }

            return services;
        }
    }
}