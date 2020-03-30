// -----------------------------------------------------------------------
//  <copyright file="MessageCreatedEventData.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-10-15 9:27</last-date>
// -----------------------------------------------------------------------

using Hybrid.EventBuses;

using LeXun.Demo.Infos.Entities;

namespace LeXun.Demo.Infos.Events
{
    /// <summary>
    /// 事件数据：发送消息
    /// </summary>
    public class MessageCreatedEventData : EventDataBase
    {
        /// <summary>
        /// 获取或设置 新增的消息
        /// </summary>
        public Message[] Messages { get; set; }
    }
}