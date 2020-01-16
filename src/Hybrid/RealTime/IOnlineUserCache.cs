// -----------------------------------------------------------------------
//  <copyright file="IOnlineUserCache.cs" company="com.esoftor">
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
    /// 定义在线用户缓存
    /// </summary>
    public interface IOnlineUserCache
    {
        /// <summary>
        /// 获取或刷新在线用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>在线用户信息</returns>
        OnlineUserBase GetOrRefresh(string userName);

        /// <summary>
        /// 异步获取或刷新在线用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>在线用户信息</returns>
        Task<OnlineUserBase> GetOrRefreshAsync(string userName);

        /// <summary>
        /// 移除在线用户信息
        /// </summary>
        /// <param name="userNames">用户名</param>
        /// <returns>移除的用户信息</returns>
        void Remove(params string[] userNames);
    }
}