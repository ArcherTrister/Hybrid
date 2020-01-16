// -----------------------------------------------------------------------
//  <copyright file="IMethodInfoFinder.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-15 2:37</last-date>
// -----------------------------------------------------------------------

using Hybrid.Dependency;

using System;
using System.Reflection;

namespace Hybrid.Reflection
{
    /// <summary>
    /// 定义方法信息查找器
    /// </summary>
    [IgnoreDependency]
    public interface IMethodInfoFinder
    {
        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="type">要查找的类型</param>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        MethodInfo[] Find(Type type, Func<MethodInfo, bool> predicate);

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <param name="type">要查找的类型</param>
        /// <returns></returns>
        MethodInfo[] FindAll(Type type);
    }
}