using Microsoft.AspNetCore.Mvc.Controllers;

using System;
using System.Linq;
using System.Reflection;

namespace Test.Web.Multiple
{
    public class HybridMultipleControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            var isController = base.IsController(typeInfo);

            if (!isController)
            {
                //string[] validEndings = new[] { "Controller`1" };

                //isController = validEndings.Any(x =>
                //    typeInfo.Name.EndsWith(x, StringComparison.OrdinalIgnoreCase));
                isController = typeInfo.Name.EndsWith("Controller`2", StringComparison.OrdinalIgnoreCase)
                    && typeInfo.GenericTypeArguments.Any();
            }
#if DEBUG
            if (isController)
            {
                Console.WriteLine($"{typeInfo.Name} IsController: {isController}.");
            }
#endif

            return isController;
        }
    }
}