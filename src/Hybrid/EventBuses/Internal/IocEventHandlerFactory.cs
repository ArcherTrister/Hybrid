// -----------------------------------------------------------------------
//  <copyright file="IocEventHandlerFactory.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-19 3:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Dependency;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.EventBuses.Internal
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