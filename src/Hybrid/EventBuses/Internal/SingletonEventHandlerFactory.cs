﻿// -----------------------------------------------------------------------
//  <copyright file="SingletonEventHandlerFactory.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-19 1:34</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.EventBuses.Internal
{
    /// <summary>
    /// 单例生命周期的事件处理器实例获取方式
    /// </summary>
    internal class SingletonEventHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// 初始化一个<see cref="SingletonEventHandlerFactory"/>类型的新实例
        /// </summary>
        public SingletonEventHandlerFactory(IEventHandler handler)
        {
            HandlerInstance = handler;
        }

        public IEventHandler HandlerInstance { get; }

        /// <summary>
        /// 获取事件处理器实例
        /// </summary>
        /// <returns></returns>
        public EventHandlerDisposeWrapper GetHandler()
        {
            return new EventHandlerDisposeWrapper(HandlerInstance);
        }
    }
}