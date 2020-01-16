using Microsoft.AspNetCore.Mvc;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    /// <summary>
    /// 周期性作业
    /// </summary>
    public class RecurringController : QuartzBaseController
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