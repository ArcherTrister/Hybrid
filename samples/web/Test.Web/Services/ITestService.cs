using Microsoft.AspNetCore.Mvc;

namespace Test.Web.Services
{
    //[Route("test")]
    public interface ITestService
    {
        [Route("{name}"), HttpGet]
        string Test(string name);
    }

    public class TestService : ITestService
    {
        public string Test(string name)
        {
            return "Hello " + name;
        }
    }
}