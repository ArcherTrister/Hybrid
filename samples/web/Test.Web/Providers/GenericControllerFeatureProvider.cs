// -----------------------------------------------------------------------
//  <copyright file="GenericControllerFeatureProvider" company="cn.lxking">
//      Copyright (c) 2014 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-13 20:33:47</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using WebApplication123.Controllers;

namespace WebApplication123
{
    public class GenericControllerFeatureProvider<T> : IApplicationFeatureProvider<ControllerFeature>
        where T : class
    {
        private IEnumerable<TypeInfo> _typeInfos;
        public GenericControllerFeatureProvider(IEnumerable<TypeInfo> typeInfos)
        {
            _typeInfos = typeInfos;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            //var controllerType = typeof(GenericController<>).MakeGenericType(typeof(T)).GetTypeInfo();
            //feature.Controllers.Add(controllerType);


            foreach (var model in _typeInfos)
            {

                // Check to see if there is a "real" controller for this class
                if (!feature.Controllers.Any(t => t.Name == model.Name || t.BaseType.Name == model.Name))
                {
                    var defaultUIAttribute = model.GetCustomAttribute<UIAttribute>();
                    if (defaultUIAttribute == null)
                    {
                        return;
                    }

                    var templateInstance = defaultUIAttribute.Template.MakeGenericType(typeof(T));

                    //var typeInfo = templateInstance.MakeGenericType(typeof(T)).GetTypeInfo();
                    var typeInfo = templateInstance.GetTypeInfo();
                    feature.Controllers.Add(typeInfo);
                }
            }

            ////var controller = classes.Single(a => a.IsClass
            ////    && a.IsPublic
            ////    && a.ContainsGenericParameters
            ////    && (a.Name.Contains("Controller") || a.IsDefined(typeof(ControllerAttribute))));

            //foreach (var model in models)
            //{
            //    var typeName = model.Name;
            //    var typeInfo = typeof(GenericController<>).MakeGenericType(typeof(T)).GetTypeInfo();
            //    feature.Controllers.Add(typeInfo);
            //}
        }
    }

    public class GenericControllerApplicationPart : ApplicationPart, IApplicationPartTypeProvider
    {
        public IEnumerable<TypeInfo> Types { get; }

        public override string Name => "GenericController";

        public GenericControllerApplicationPart(IEnumerable<TypeInfo> typeInfos, Type entityType)
        {
            var aaa = typeInfos.Select(p => p.GetCustomAttribute<UIAttribute>());
            Types = typeInfos.Select(p => p.GetCustomAttribute<UIAttribute>())
                .Select(m => m.Template.MakeGenericType(entityType))
                .Select(t => t.GetTypeInfo())
                .ToArray();
        }
    }
}
