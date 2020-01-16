// -----------------------------------------------------------------------
//  <copyright file="MvcMethodInfoFinder.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Extensions;
using ESoftor.Reflection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ESoftor.AspNetCore.Mvc
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
            //while (IsController(type))
            while (type.IsController())
            {
                types.AddIfNotExist(type);
                type = type?.BaseType;
                if (type?.Name == "Controller" || type?.Name == "ControllerBase")
                {
                    break;
                }
            }

            return types.SelectMany(m => m.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)).ToArray();
        }
    }
}