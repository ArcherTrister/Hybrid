using Microsoft.Extensions.DependencyInjection;

using Quartz;
using Quartz.Spi;

using System;

namespace Hybrid.Quartz
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                //if (_serviceProvider.GetService(bundle.JobDetail.JobType) is IJob job) return job;
                var job = (IJob)_serviceProvider.GetService(bundle.JobDetail.JobType);
                if (job != null)
                {
                    return job;
                }

                return ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, bundle.JobDetail.JobType) as IJob;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException($"从容器IServiceProvider中构建Job:{bundle.JobDetail.JobType.FullName}失败", ex.Message);
            }
        }

        public void ReturnJob(IJob job)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}