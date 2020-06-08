// -----------------------------------------------------------------------
//  <copyright file="HybridControllerFeatureProvider.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.DynamicWebApi.Attributes;
using Hybrid.Data;
using Hybrid.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;

using System;
using System.Linq;
using System.Reflection;

namespace Hybrid.AspNetCore.Mvc
{
    internal class HybridControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            var isController = base.IsController(typeInfo);

            if (!isController)
            {
                isController = HybridConstants.CustomController.ValidEndings.Any(x =>
                    typeInfo.Name.EndsWith(x, StringComparison.OrdinalIgnoreCase))
                    && typeInfo.GenericTypeArguments.Any();
            }

            if (!isController)
            {
                isController = typeInfo.HasAttribute<DynamicWebApiAttribute>();
            }

#if DEBUG
            if (isController)
            {
                Console.WriteLine($"{typeInfo.Name} IsController: {isController}.");
            }
#endif
            return isController;
        }
    }
}