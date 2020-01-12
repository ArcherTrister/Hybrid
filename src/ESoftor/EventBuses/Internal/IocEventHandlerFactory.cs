﻿// -----------------------------------------------------------------------
//  <copyright file="IocEventHandlerFactory.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-19 3:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Dependency;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace ESoftor.EventBuses.Internal
{
    /// <summary>
    /// 依赖注入事件处理器实例获取方式
    /// </summary>
    internal class IocEventHandlerFactory : IEventHandlerFactory
    {
        private readonly IHybridServiceScopeFactory _serviceScopeFactory;
        private readonly Type _handlerType;

        /// <summary>
        /// 初始化一个<see cref="IocEventHandlerFactory"/>类型的新实例
        /// </summary>
        /// <param name="serviceScopeFactory">服务作用域工厂</param>
        /// <param name="handlerType">事件处理器类型</param>
        public IocEventHandlerFactory(IHybridServiceScopeFactory serviceScopeFactory, Type handlerType)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _handlerType = handlerType;
        }

        /// <summary>
        /// 获取事件处理器实例
        /// </summary>
        /// <returns></returns>
        public EventHandlerDisposeWrapper GetHandler()
        {
            IServiceScope scope = _serviceScopeFactory.CreateScope();
            return new EventHandlerDisposeWrapper((IEventHandler)scope.ServiceProvider.GetService(_handlerType), () => scope.Dispose());
        }
    }
}