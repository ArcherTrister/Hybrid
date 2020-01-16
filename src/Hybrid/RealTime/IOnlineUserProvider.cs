﻿// -----------------------------------------------------------------------
//  <copyright file="IOnlineUserProvider.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-01 23:32</last-date>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace Hybrid.RealTime
{
    /// <summary>
    /// 定义在线用户提供者
    /// </summary>
    public interface IOnlineUserProvider
    {
        /// <summary>
        /// 获取或创建在线用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>在线用户信息</returns>
        Task<OnlineUserBase> GetOrCreate(string userName);

        /// <summary>
        /// 移除在线用户信息
        /// </summary>
        /// <param name="userNames">用户名</param>
        void Remove(params string[] userNames);
    }
}