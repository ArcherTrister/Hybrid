using Hybrid.Quartz.Dashboard.Models.Dtos;

using Quartz;
using Quartz.Impl;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Quartz
{
    public class SchedulerManager : ISchedulerManager
    {
        public async Task<IReadOnlyList<SchedulerHeaderDto>> GetSchedulers()
        {
            IReadOnlyList<IScheduler> schedulers = await SchedulerRepository.Instance.LookupAll().ConfigureAwait(false);
            return schedulers.Select(x => new SchedulerHeaderDto(x)).ToList();
        }

        public async Task<SchedulerHeaderDto> GetCurrentScheduler(string currentSchedulerName)
        {
            IReadOnlyList<IScheduler> schedulers = await SchedulerRepository.Instance.LookupAll().ConfigureAwait(false);
            List<SchedulerHeaderDto> schedulerHeaders = schedulers.Select(x => new SchedulerHeaderDto(x)).ToList();
            if (schedulerHeaders.Count <= 0)
            {
                throw new Exception("No scheduler defined in this application.");
            }

            if (string.IsNullOrEmpty(currentSchedulerName))
            {
                return schedulerHeaders[0];
            }

            //Try to find exact match
            SchedulerHeaderDto currentScheduler = schedulerHeaders.FirstOrDefault(s => s.Name.Equals(currentSchedulerName));
            if (currentScheduler != null)
            {
                return currentScheduler;
            }

            //Try to find best match
            currentScheduler = schedulerHeaders.FirstOrDefault(s => currentSchedulerName.StartsWith(s.Name, StringComparison.OrdinalIgnoreCase));
            return currentScheduler ?? schedulerHeaders[0];

            //Try to find default language

            //Get first one
        }
    }
}