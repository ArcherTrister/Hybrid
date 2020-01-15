using ESoftor.Data;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

using System;
using System.Linq;

namespace ESoftor.Zero.UI
{
    internal class HybridApplicationModelConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            var removeList = application.Controllers.Where(p =>
                ESoftorConstants.CustomController.ValidEndings.Any(x=> 
                    p.ControllerName.EndsWith(x, StringComparison.OrdinalIgnoreCase))
                && !p.ControllerType.GenericTypeArguments.Any()).ToList();
            foreach (var item in removeList)
            {
                application.Controllers.Remove(item);
            }
        }
    }
}
