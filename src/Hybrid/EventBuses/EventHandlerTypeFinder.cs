﻿// -----------------------------------------------------------------------
//  <copyright file="EventHandlerTypeFinder.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-28 23:23</last-date>
// -----------------------------------------------------------------------

using Hybrid.Finders;
using Hybrid.Reflection;

using System;
using System.Linq;

namespace Hybrid.EventBuses
{
    /// <summary>
    /// 事件处理器类型查找器
    /// </summary>
    public class EventHandlerTypeFinder : FinderBase<Type>, IEventHandlerTypeFinder
    {
        private readonly IAllAssemblyFinder _allAssemblyFinder;

        /// <summary>
        /// 初始化一个<see cref="EventHandlerTypeFinder"/>类型的新实例
        /// </summary>
        public EventHandlerTypeFinder(IAllAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            Type baseType = typeof(IEventHandler<>);
            return _allAssemblyFinder.FindAll(true).SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsDeriveClassFrom(baseType)).Distinct().ToArray();
        }
    }
}