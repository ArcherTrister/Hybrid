using ESoftor.AspNetCore.Mvc.Controllers;

using Microsoft.AspNetCore.Mvc;

namespace ESoftor.Zero.UI.Quickstart
{
    [HybridDefaultUI(typeof(HybridController<>))]
    public abstract class HybridController : MvcController
    {
    }

    [Route("Hybrid")]
    //[Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HybridController<T> : HybridController where T : class
    {
        //private readonly IDoService<T> _doService;
        //public TestController(IDoService<T> doService)
        //{
        //    _doService = doService;
        //}

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag["type"] = typeof(T).Name;
            return View();
        }
    }
}