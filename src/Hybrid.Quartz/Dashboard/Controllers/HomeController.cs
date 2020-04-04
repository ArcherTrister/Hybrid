using Microsoft.AspNetCore.Mvc;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    /// <summary>
    /// 定时任务首页
    /// </summary>
    [Route("Quartz")]
    public sealed class HomeController : QuartzBaseController
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
