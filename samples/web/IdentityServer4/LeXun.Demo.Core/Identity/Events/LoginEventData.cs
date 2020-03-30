// -----------------------------------------------------------------------
//  <copyright file="LoginEventData.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.EventBuses;

using LeXun.Demo.Identity.Dtos;
using LeXun.Demo.Identity.Entities;

namespace LeXun.Demo.Identity.Events
{
    /// <summary>
    /// 登录事件数据
    /// </summary>
    public class LoginEventData : EventDataBase
    {
        /// <summary>
        /// 获取或设置 登录信息
        /// </summary>
        public LoginDto LoginDto { get; set; }

        /// <summary>
        /// 获取或设置 登录用户
        /// </summary>
        public User User { get; set; }
    }
}