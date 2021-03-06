﻿// -----------------------------------------------------------------------
//  <copyright file="MvcMethodInfoFinder.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-02-23 19:59</last-date>
// -----------------------------------------------------------------------

using Hybrid.Collections;
using Hybrid.Reflection;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hybrid.AspNetCore.Mvc
{
    /// <summary>
    /// MVC方法查找器
    /// </summary>
    public class MvcMethodInfoFinder : IMethodInfoFinder
    {
        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="type">要查找的类型</param>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public MethodInfo[] Find(Type type, Func<MethodInfo, bool> predicate)
        {
            return FindAll(type).Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <param name="type">要查找的类型</param>
        /// <returns></returns>
        public MethodInfo[] FindAll(Type type)
        {
            List<Type> types = new List<Type>();
            while (IsController(type))
            {
                types.AddIfNotExist(type);
                type = type?.BaseType;
                if ("Controller".Equals(type?.Name) || "ControllerBase".Equals(type?.Name))
                {
                    break;
                }
            }

            return types.SelectMany(m => m.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)).ToArray();
        }

        private static bool IsController(Type type)
        {
            return type != null && type.IsClass && type.IsPublic && !type.ContainsGenericParameters && !type.IsAbstract
                && !type.IsDefined(typeof(NonControllerAttribute)) 
                && (type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) 
                || type.Name.EndsWith("ControllerBase", StringComparison.OrdinalIgnoreCase) 
                || type.Name.EndsWith("AppService", StringComparison.OrdinalIgnoreCase) 
                || type.Name.EndsWith("ApplicationService", StringComparison.OrdinalIgnoreCase) 
                || type.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase)
                    || type.IsDefined(typeof(ControllerAttribute)));
        }
    }
}