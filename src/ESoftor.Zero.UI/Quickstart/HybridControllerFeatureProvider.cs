using ESoftor.Data;
using Microsoft.AspNetCore.Mvc.Controllers;

using System;
using System.Linq;
using System.Reflection;

namespace ESoftor.Zero.IdentityServer4.Quickstart
{
    internal class HybridControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            var isController = base.IsController(typeInfo);

            if (!isController)
            {
                isController = ESoftorConstants.CustomController.ValidEndings.Any(x =>
                    typeInfo.Name.EndsWith(x, StringComparison.OrdinalIgnoreCase)) 
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