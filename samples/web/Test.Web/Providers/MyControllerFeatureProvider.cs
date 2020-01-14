using Microsoft.AspNetCore.Mvc.Controllers;

using System;
using System.Linq;
using System.Reflection;

namespace Test.Web.Providers
{
    public class MyControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            var isController = base.IsController(typeInfo);

            if (!isController)
            {
                string[] validEndings = new[] { "Foobar", "Controller`1" };

                isController = validEndings.Any(x =>
                    typeInfo.Name.EndsWith(x, StringComparison.OrdinalIgnoreCase));
            }
            if (isController)
            {
                Console.WriteLine($"{typeInfo.Name} IsController: {isController}.");
            }

            return isController;
        }
    }
}