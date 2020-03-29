// -----------------------------------------------------------------------
//  <copyright file="ConnectionUser.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
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