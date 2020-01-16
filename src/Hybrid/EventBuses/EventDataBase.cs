// -----------------------------------------------------------------------
//  <copyright file="EventData.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-18 12:40</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.EventBuses
{
    /// <summary>
    /// 事件源数据信息基类
    /// </summary>
    public abstract class EventDataBase : IEventData
    {
        /// <summary>
        /// 初始化一个<see cref="EventDataBase"/>类型的新实例
        /// </summary>
        protected EventDataBase()
        {
            Id = Guid.NewGuid();
            EventTime = DateTime.Now;
        }

        /// <summary>
        /// 获取 事件编号
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// 获取 事件发生时间
        /// </summary>
        public DateTime EventTime { get; }

        /// <summary>
        /// 获取或设置 触发事件的对象
        /// </summary>
        public object EventSource { get; set; }
    }
}