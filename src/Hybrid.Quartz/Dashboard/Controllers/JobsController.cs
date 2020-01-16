using Hybrid.Quartz.Dashboard.Models.Dtos;

using Microsoft.AspNetCore.Mvc;

using Quartz;

using System.Threading.Tasks;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    /// <summary>
    /// 定时任务
    /// </summary>
    public class JobsController : QuartzBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(string schedulerName, string jobGroup, string jobName)
        {
            IScheduler scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            IJobDetail jobDetail = await scheduler.GetJobDetail(new JobKey(jobName, jobGroup)).ConfigureAwait(false);
            if (jobDetail == null)
                return View(null);
            return View(new JobDetailDto(jobDetail, schedulerName));
        }

        public IActionResult Queued()
        {
            return View();
        }

        public IActionResult Plan()
        {
            return View();
        }

        public IActionResult Executing()
        {
            return View();
        }

        public IActionResult Complete()
        {
            return View();
        }

        public IActionResult Fail()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Waiting()
        {
            return View();
        }
    }
}