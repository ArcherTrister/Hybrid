// -----------------------------------------------------------------------
//  <copyright file="HybridControllerModelConvention.cs" company="cn.lxking">
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
    internal class HybridControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel model)
        {
            bool defaultUIAttribute = HybridConstants.CustomController.ValidEndings.Any(x =>
                    model.ControllerName.EndsWith(x, StringComparison.OrdinalIgnoreCase));
            if (defaultUIAttribute)
            {
                model.ControllerName = model.ControllerType.BaseType.Name.Replace("Controller", string.Empty);
            }
        }
    }
}
