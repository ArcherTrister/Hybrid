using Microsoft.AspNetCore.Mvc.Razor;

using System.Collections.Generic;
using System.Linq;

namespace Hybrid.Quartz.Dashboard
{
    internal class HybridQuartzViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            string[] locations = new string[] {
                "/Dashboard/Views/{1}/{0}.cshtml",
                "/Dashboard/Views/Shared/{0}.cshtml"
            };
            return locations.Union(viewLocations);
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["QuartzViewLocations"] = nameof(HybridQuartzViewLocationExpander);
        }
    }
}