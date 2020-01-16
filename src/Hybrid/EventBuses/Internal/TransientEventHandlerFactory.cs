// -----------------------------------------------------------------------
//  <copyright file="TransientEventHandlerFactory.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-19 1:31</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.EventBuses.Internal
{
    /// <summary>
    /// 即时生命周期的事件处理器实例获取方式
    /// </summary>
    internal class TransientEventHandlerFactory<TEventHandler> : IEventHandlerFactory
        where TEventHandler : IEventHandler, new()
    {
        /// <summary>
        /// 获取事件处理器实例
        /// </summary>
        /// <returns></returns>
        public EventHandlerDisposeWrapper GetHandler()
        {
            IEventHandler handler = new TEventHandler();
            return new EventHandlerDisposeWrapper(handler, () => (handler as IDisposable)?.Dispose());
        }
    }
}