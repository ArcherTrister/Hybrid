using Microsoft.AspNetCore.Mvc;

using Hybrid.AspNetCore.Mvc.Controllers;
using Hybrid.Localization;

using Quartz;
using Quartz.Impl;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    /// <summary>
    /// Base class for all MVC Controllers in Quartz system.
    /// </summary>
    [Area("Quartz")]
    public class QuartzBaseController : MvcController
    {
        public QuartzBaseController()
        {
            LocalizationSourceName = LocalizationConsts.QuartzSourceName;
        }

        #region 私有方法

        internal static async Task<IScheduler> GetScheduler(string schedulerName)
        {
            IScheduler scheduler = await SchedulerRepository.Instance.Lookup(schedulerName).ConfigureAwait(false);
            if (scheduler == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                throw new KeyNotFoundException($"Scheduler {schedulerName} not found!");
            }
            return scheduler;
        }

        #endregion 私有方法
    }
}