// -----------------------------------------------------------------------
//  <copyright file="IEventData.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-01-12 11:43</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.EventBuses
{
    /// <summary>
    /// 定义事件数据，所有事件都要实现该接口
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// 获取 事件编号
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 获取 事件发生的时间
        /// </summary>
        DateTime EventTime { get; }

        /// <summary>
        /// 获取或设置 事件源，触发事件的对象
        /// </summary>
        object EventSource { get; set; }
    }
}