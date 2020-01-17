using Microsoft.AspNetCore.Mvc;

using System;

using Test.Web.Models;
using Test.Web.Multiple;

namespace WebApplication123.Controllers
{
    [HybridMultipleUI(typeof(MultipleTestController<,>))]
    public abstract class MultipleTestController : Controller
    {
    }

    [Route("MultipleTest")]
    //[Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    internal class MultipleTestController<TUser, TUserKey> : MultipleTestController
        where TUser : UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
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
            return Content($"Type Name Is {typeof(TUser).Name}, Key Name Is {typeof(TUserKey).Name}");
        }
    }
}