﻿// -----------------------------------------------------------------------
//  <copyright file="GenericControllerNameAttribute" company="cn.lxking">
//      Copyright (c) 2014 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-13 20:32:00</last-date>
// -----------------------------------------------------------------------

using ESoftor.Data;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

using System;
using System.Linq;

namespace ESoftor.Zero.UI
{
    internal class HybridControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel model)
        {
            bool defaultUIAttribute = ESoftorConstants.CustomController.ValidEndings.Any(x =>
                    model.ControllerName.EndsWith(x, StringComparison.OrdinalIgnoreCase));
            if (defaultUIAttribute)
            {
                model.ControllerName = model.ControllerType.BaseType.Name;
            }
        }
    }
}
