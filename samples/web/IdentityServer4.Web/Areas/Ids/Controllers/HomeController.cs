using Hybrid.AspNetCore.Mvc.Controllers;

using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4.Web.Areas.Ids.Controllers
{
    [Area("Ids")]
    public class HomeController : MvcController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}