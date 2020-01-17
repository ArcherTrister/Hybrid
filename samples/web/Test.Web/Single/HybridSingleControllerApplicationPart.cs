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

namespace Test.Web.Single
{
    public class HybridSingleControllerApplicationPart : ApplicationPart, IApplicationPartTypeProvider
    {
        public IEnumerable<TypeInfo> Types { get; }

        public override string Name => "GenericController";

        public HybridSingleControllerApplicationPart(IEnumerable<TypeInfo> typeInfos, Type entityType)
        {
            var aaa = typeInfos.Select(p => p.GetCustomAttribute<HybridDefaultUIAttribute>());
            Types = typeInfos.Select(p => p.GetCustomAttribute<HybridDefaultUIAttribute>())
                .Select(m => m.Template.MakeGenericType(entityType))
                .Select(t => t.GetTypeInfo())
                .ToArray();
        }
    }
}