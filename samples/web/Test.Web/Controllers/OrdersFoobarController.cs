using Microsoft.AspNetCore.Mvc;

using System;

namespace Test.Web.Controllers
{
    //public class OrdersFoobarController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //}
    [Route("OrdersFoobar")]
    public class OrdersFoobarController<T>
        where T : class
    {
        [HttpGet]
        public ActionResult Get()
        {
            Console.WriteLine("");
            return new OkResult();
        }
    }
}