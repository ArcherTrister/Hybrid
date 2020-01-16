// -----------------------------------------------------------------------
//  <copyright file="HybridApplicationModelConvention.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Data;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

using System;
using System.Linq;

namespace ESoftor.Zero.IdentityServer4.Quickstart
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
