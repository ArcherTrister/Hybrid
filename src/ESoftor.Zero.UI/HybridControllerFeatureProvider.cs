using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    internal class HybridControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            var isController = base.IsController(typeInfo);

            if (!isController)
            {
                //string[] validEndings = new[] { "Controller`1" };

                //isController = validEndings.Any(x =>
                //    typeInfo.Name.EndsWith(x, StringComparison.OrdinalIgnoreCase));

                isController = typeInfo.Name.EndsWith("Controller`1", StringComparison.OrdinalIgnoreCase);
            }
#if DEBUG
            if (isController)
            {
                if (typeInfo.Name.StartsWith("Hybrid", StringComparison.OrdinalIgnoreCase))
                { 
                
                }
                Console.WriteLine($"{typeInfo.Name} IsController: {isController} type： {typeInfo.IsPublic} {typeInfo.IsNotPublic}.");
            }
#endif
            return isController;
        }
    }
}