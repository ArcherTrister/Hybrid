// -----------------------------------------------------------------------
//  <copyright file="GenericControllerFeatureProvider" company="cn.lxking">
//      Copyright (c) 2014 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-13 20:33:47</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationParts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Test.Web.Multiple
{
    public class HybridMultipleControllerApplicationPart : ApplicationPart, IApplicationPartTypeProvider
    {
        public IEnumerable<TypeInfo> Types { get; }

        public override string Name => "HybridMultipleController";

        public HybridMultipleControllerApplicationPart(IEnumerable<TypeInfo> typeInfos, Type entityType, Type keyType)
        {
            Types = typeInfos.Select(p => p.GetCustomAttribute<HybridMultipleUIAttribute>())
                .Select(m => m.EntityType.MakeGenericType(entityType, keyType))
                .Select(t => t.GetTypeInfo())
                .ToArray();
        }
    }
}