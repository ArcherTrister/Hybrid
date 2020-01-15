using Microsoft.AspNetCore.Mvc;

using Test.Web.Single;

namespace WebApplication123.Controllers
{
    [HybridDefaultUI(typeof(TestController<>))]
    public abstract class TestController : Controller
    {
    }

    [Route("SingleTest")]
    [ApiExplorerSettings(IgnoreApi = true)]
    internal class TestController<T> : TestController where T : class
    {
        //private readonly IDoService<T> _doService;
        //public TestController(IDoService<T> doService)
        //{
        //    _doService = doService;
        //}

        [HttpGet]
        public IActionResult Index()
        {
            //_doService.SayHello();
            //Console.WriteLine();
            //return View();
            return Content($"Type Name Is {typeof(T).Name}");
        }
    }
}