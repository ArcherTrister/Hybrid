using Hybrid.Dependency;
using Hybrid.Quartz;

using Microsoft.Extensions.DependencyInjection.Extensions;

using System;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册Job
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InitializeJobs(this IServiceCollection services)
        {
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