using Microsoft.AspNetCore.Mvc;
using Test.Web.Services;

namespace WebApplication123.Controllers
{
    [UIAttribute(typeof(TestController<>))]
    public abstract class TestController : Controller
    {
    }

    [Route("Test")]
    //[Route("[controller]")]
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
            return View();
        }
    }
}