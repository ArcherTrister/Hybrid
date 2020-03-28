// -----------------------------------------------------------------------
//  <copyright file="ConnectionUser.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-01-04 20:28</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Hybrid.AspNetCore.SignalR
{
    /// <summary>
    /// SignalR 连接用户项
    /// </summary>
    public class ConnectionUser
    {
        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 连接Id集合
        /// </summary>
        public ICollection<string> ConnectionIds { get; set; } = new List<string>();
    }
}