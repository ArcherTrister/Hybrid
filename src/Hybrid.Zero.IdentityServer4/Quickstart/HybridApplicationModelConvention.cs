// -----------------------------------------------------------------------
//  <copyright file="HybridApplicationModelConvention.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;

using Microsoft.AspNetCore.Mvc.ApplicationModels;

using System;
using System.Linq;

namespace Hybrid.Zero.IdentityServer4.Quickstart
{
    internal class HybridApplicationModelConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            var removeList = application.Controllers.Where(p =>
                HybridConsts.CustomController.ValidEndings.Any(x =>
                    p.ControllerName.EndsWith(x, StringComparison.OrdinalIgnoreCase))
                && !p.ControllerType.GenericTypeArguments.Any()).ToList();
            foreach (var item in removeList)
            {
                application.Controllers.Remove(item);
            }
        }
    }
}