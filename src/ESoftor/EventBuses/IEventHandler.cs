﻿// -----------------------------------------------------------------------
//  <copyright file="IEventHandler.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-01-12 12:10</last-date>
// -----------------------------------------------------------------------

using ESoftor.Dependency;

using System.Threading;
using System.Threading.Tasks;

namespace ESoftor.EventBuses
{
    /// <summary>
    /// 定义事件处理器，所有事件处理都要实现该接口
    /// EventBus中，Handler的调用是同步执行的，如果需要触发就不管的异步执行，可以在实现EventHandler的Handle逻辑时使用Task.Run
    /// </summary>
    [IgnoreDependency]
    public interface IEventHandler
    {
        /// <summary>
        /// 是否可处理指定事件
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        /// <returns>是否可处理</returns>
        bool CanHandle(IEventData eventData);

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        void Handle(IEventData eventData);

        /// <summary>
        /// 异步事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        /// <param name="cancelToken">异步取消标识</param>
        /// <returns></returns>
        Task HandleAsync(IEventData eventData, CancellationToken cancelToken = default(CancellationToken));
    }

    /// <summary>
    /// 定义泛型事件处理器
    /// EventBus中，Handler的调用是同步执行的，如果需要触发就不管的异步执行，可以在实现EventHandler的Handle逻辑时使用Task.Run
    /// </summary>
    /// <typeparam name="TEventData">事件源数据</typeparam>
    [IgnoreDependency]
    public interface IEventHandler<in TEventData> : IEventHandler where TEventData : IEventData
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        void Handle(TEventData eventData);

        /// <summary>
        /// 异步事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        /// <param name="cancelToken">异步取消标识</param>
        Task HandleAsync(TEventData eventData, CancellationToken cancelToken = default(CancellationToken));
    }
}