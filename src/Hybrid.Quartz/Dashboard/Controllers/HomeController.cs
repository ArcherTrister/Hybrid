using Hybrid.AspNetCore.Mvc.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace Hybrid.Quartz.Dashboard.Controllers
{
    public class HomeController : QuartzBaseController
    {
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new HybridErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}