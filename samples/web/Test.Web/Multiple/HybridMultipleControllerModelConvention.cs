// -----------------------------------------------------------------------
//  <copyright file="GenericControllerNameAttribute" company="cn.lxking">
//      Copyright (c) 2014 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-13 20:32:00</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

using System;

namespace Test.Web.Multiple
{
    public class HybridMultipleControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel model)
        {
            bool defaultUIAttribute = model.ControllerName.EndsWith("Controller`2", StringComparison.OrdinalIgnoreCase);
            if (defaultUIAttribute)
            {
                model.ControllerName = model.ControllerType.BaseType.Name;
            }
        }
    }
}
