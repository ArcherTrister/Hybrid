using Hybrid.Extensions;

using Quartz;
using Quartz.Impl;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard
{
    /// <summary>
    ///
    /// </summary>
    public static class GlobalizationHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="schedulerCode">调度名称</param>
        /// <returns></returns>
        public async static Task<bool> IsValidSchedulerName(string schedulerName)
        {
            if (schedulerName.IsNullOrWhiteSpace())
            {
                return false;
            }

            IReadOnlyList<IScheduler> schedulers = await SchedulerRepository.Instance.LookupAll().ConfigureAwait(false);
            return schedulers.Where(s => s.SchedulerName.Equals(schedulerName)).Any();
        }
    }
}