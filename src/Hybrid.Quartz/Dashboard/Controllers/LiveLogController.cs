using Microsoft.AspNetCore.Mvc;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    /// <summary>
    /// 实况日志
    /// </summary>
    public class LiveLogController : QuartzBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}