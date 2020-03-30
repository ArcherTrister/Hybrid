// -----------------------------------------------------------------------
//  <copyright file="LogoutEventData.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.EventBuses;

namespace LeXun.Demo.Identity.Events
{
    /// <summary>
    /// 用户退出事件数据
    /// </summary>
    public class LogoutEventData : EventDataBase
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public int UserId { get; set; }
    }
}