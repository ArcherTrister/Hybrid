using Microsoft.AspNetCore.Mvc.ApplicationModels;

using System;
using System.Linq;

namespace ESoftor.Zero.UI
{
    internal class HybridApplicationModelConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            var removeList = application.Controllers.Where(p => p.ControllerName.EndsWith("Controller`1", StringComparison.OrdinalIgnoreCase) && !p.ControllerType.GenericTypeArguments.Any()).ToList();
            foreach (var item in removeList)
            {
                application.Controllers.Remove(item);
            }
        }
    }
}
