﻿// -----------------------------------------------------------------------
//  <copyright file="IEventHandlerFactory.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-19 1:27</last-date>
// -----------------------------------------------------------------------

using ESoftor.EventBuses.Internal;

namespace ESoftor.EventBuses
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