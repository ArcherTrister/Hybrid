// -----------------------------------------------------------------------
//  <copyright file="HybridControllerModelConvention.cs" company="com.esoftor">
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
    internal class HybridControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel model)
        {
            bool defaultUIAttribute = ESoftorConstants.CustomController.ValidEndings.Any(x =>
                    model.ControllerName.EndsWith(x, StringComparison.OrdinalIgnoreCase));
            if (defaultUIAttribute)
            {
                model.ControllerName = model.ControllerType.BaseType.Name.Replace("Controller", string.Empty);
            }
        }
    }
}
