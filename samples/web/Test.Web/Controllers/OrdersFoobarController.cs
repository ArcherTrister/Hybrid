using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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