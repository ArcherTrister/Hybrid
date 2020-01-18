using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Test.Web.Controllers
{
    public class ValidateController : Controller
    {
        public ValidateController(IOptions<CustomConfig> config, ILogger<ValidateController> logger)
        {
            try
            {
                var configValue = config.Value;
            }
            catch (OptionsValidationException ex)
            {
                foreach (var failure in ex.Failures)
                {
                    logger.LogError(failure);
                }
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}