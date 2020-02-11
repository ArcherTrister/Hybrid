using Microsoft.AspNetCore.Mvc;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    /// <summary>
    /// 历史记录
    /// </summary>
    public class HistoryController : QuartzBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}