using Microsoft.AspNetCore.Mvc;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    /// <summary>
    /// Cron触发器
    /// </summary>
    public class CronTriggerController : QuartzBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}