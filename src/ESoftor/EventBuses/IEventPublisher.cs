// -----------------------------------------------------------------------
//  <copyright file="IEventPublisher.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-01 17:55</last-date>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace ESoftor.EventBuses
{
    /// <summary>
    /// 定义事件发布者
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// 同步发布指定事件
        /// </summary>
        /// <typeparam name="TEventData">事件数据类型</typeparam>
        /// <param name="eventData">事件数据</param>
        /// <param name="wait">是否等待结果返回</param>
        void Publish<TEventData>(TEventData eventData, bool wait = true) where TEventData : IEventData;

        /// <summary>
        /// 同步发布指定事件，并指定事件源
        /// </summary>
        /// <typeparam name="TEventData">事件数据类型</typeparam>
        /// <param name="eventSource">事件源，触发事件的对象</param>
        /// <param name="eventData">事件数据</param>
        /// <param name="wait">是否等待结果返回</param>
        void Publish<TEventData>(object eventSource, TEventData eventData, bool wait = true) where TEventData : IEventData;

        /// <summary>
        /// 同步发布指定事件
        /// </summary>
        /// <param name="eventType">事件数据类型</param>
        /// <param name="eventData">事件数据</param>
        /// <param name="wait">是否等待结果返回</param>
        void Publish(Type eventType, IEventData eventData, bool wait = true);

        /// <summary>
        /// 同步发布指定事件，并指定事件源
        /// </summary>
        /// <param name="eventType">事件数据类型</param>
        /// <param name="eventSource">事件源，触发事件的对象</param>
        /// <param name="eventData">事件数据</param>
        /// <param name="wait">是否等待结果返回</param>
        void Publish(Type eventType, object eventSource, IEventData eventData, bool wait = true);

        /// <summary>
        /// 异步发布指定事件
        /// </summary>
        /// <typeparam name="TEventData">事件数据类型</typeparam>
        /// <param name="eventData">事件数据</param>
        /// <param name="wait">是否等待结果返回</param>
        Task PublishAsync<TEventData>(TEventData eventData, bool wait = true) where TEventData : IEventData;

        /// <summary>
        /// 异步发布指定事件，并指定事件源
        /// </summary>
        /// <typeparam name="TEventData">事件数据类型</typeparam>
        /// <param name="eventSource">事件源，触发事件的对象</param>
        /// <param name="eventData">事件数据</param>
        /// <param name="wait">是否等待结果返回</param>
        Task PublishAsync<TEventData>(object eventSource, TEventData eventData, bool wait = true) where TEventData : IEventData;

        /// <summary>
        /// 异步发布指定事件
        /// </summary>
        /// <param name="eventType">事件数据类型</param>
        /// <param name="eventData">事件数据</param>
        /// <param name="wait">是否等待结果返回</param>
        Task PublishAsync(Type eventType, IEventData eventData, bool wait = true);

        /// <summary>
        /// 异步发布指定事件，并指定事件源
        /// </summary>
        /// <param name="eventType">事件数据类型</param>
        /// <param name="eventSource">事件源，触发事件的对象</param>
        /// <param name="eventData">事件数据</param>
        /// <param name="wait">是否等待结果返回</param>
        Task PublishAsync(Type eventType, object eventSource, IEventData eventData, bool wait = true);
    }
}