// -----------------------------------------------------------------------
//  <copyright file="IEventHandlerFactory.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-19 1:27</last-date>
// -----------------------------------------------------------------------

using Hybrid.EventBuses.Internal;


namespace Hybrid.EventBuses
{
    /// <summary>
    /// 定义获取<see cref="IEventHandler"/>实例的方式
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// 获取事件处理器实例
        /// </summary>
        /// <returns></returns>
        EventHandlerDisposeWrapper GetHandler();
    }
}