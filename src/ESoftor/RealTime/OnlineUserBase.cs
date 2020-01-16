// -----------------------------------------------------------------------
//  <copyright file="OnlineUserBase.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-02 0:01</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace ESoftor.RealTime
{
    /// <summary>
    /// 在线用户信息基类
    /// </summary>
    public abstract class OnlineUserBase
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 用户Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 用户头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 获取或设置 是否管理
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 获取或设置 下次请求是否刷新AccessToken
        /// </summary>
        public bool IsRefreshNext { get; set; }

        /// <summary>
        /// 获取或设置 用户角色
        /// </summary>
        public string[] Roles { get; set; } = new string[0];

        /// <summary>
        /// 获取 扩展数据字典
        /// </summary>
        public IDictionary<string, string> ExtendData { get; } = new Dictionary<string, string>();
    }
}