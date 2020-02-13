using Hybrid.AspNetCore.Mvc.Controllers;

using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4.Web.Controllers
{
    public class HomeController : MvcController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}