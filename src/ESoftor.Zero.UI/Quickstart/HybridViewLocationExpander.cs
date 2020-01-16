// -----------------------------------------------------------------------
//  <copyright file="HybridViewLocationExpander.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Razor;

using System.Collections.Generic;
using System.Linq;

namespace ESoftor.Zero.IdentityServer4.Quickstart
{
    internal class HybridViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            string[] locations = new string[] {
                "/Quickstart/Views/{1}/{0}.cshtml",
                "/Quickstart/Views/Shared/{0}.cshtml"
            };
            return locations.Union(viewLocations);
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["CustomViewLocations"] = nameof(HybridViewLocationExpander);
        }
    }
}