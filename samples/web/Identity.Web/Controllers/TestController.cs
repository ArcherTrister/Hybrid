using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel;
using System.Linq;

namespace ESoftor.Web.Controllers
{
    [Description("网站-测试")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = IdentityServerConstants.LocalApi.AuthenticationScheme)]
    public class TestController : ControllerBase
    {
        //private readonly UserManager<User> _userManager;
        //private readonly IIdentityContract _identityContract;

        //public TestController(UserManager<User> userManager, IIdentityContract identityContract)
        //{
        //    _userManager = userManager;
        //    _identityContract = identityContract;
        //}

        [HttpGet]
        //[Authorize(AuthenticationSchemes = IdentityServerConstants.LocalApi.AuthenticationScheme)]
        //[Authorize]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return new JsonResult(
                from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
