using Hybrid.AspNetCore.Mvc.Controllers;

using IdentityServer4.Services;
using IdentityServer4.Stores;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace LeXun.Demo.Web.Controllers
{
    public class AuthCenterTestController : MvcController
    {
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly IPersistedGrantService _persistedGrantService;

        public AuthCenterTestController(IClientStore clientStore, IResourceStore resourceStore, IPersistedGrantService persistedGrantService)
        {
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _persistedGrantService = persistedGrantService;
        }

        public async Task<IActionResult> Index()
        {
            await _clientStore.FindClientByIdAsync("");
            return View();
        }
    }
}