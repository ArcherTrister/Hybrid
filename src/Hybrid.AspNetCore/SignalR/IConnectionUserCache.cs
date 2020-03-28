// -----------------------------------------------------------------------
//  <copyright file="IConnectionUserCache.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-01-04 20:29</last-date>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace Hybrid.AspNetCore.SignalR
{
    /// <summary>
    /// 定义SignalR连接用户缓存
    /// </summary>
    public interface IConnectionUserCache
    {
        /// <summary>
        /// 获取缓存的用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        Task<ConnectionUser> GetUser(string userName);

        /// <summary>
        /// 获取指定用户的所有连接Id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        Task<string[]> GetConnectionIds(string userName);

        /// <summary>
        /// 设置用户缓存
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        Task SetUser(string userName, ConnectionUser user);

        /// <summary>
        /// 添加指定用户的连接
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="connectionId">连接Id</param>
        /// <returns></returns>
        Task AddConnectionId(string userName, string connectionId);

        /// <summary>
        /// 移除指定用户的连接
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="connectionId">连接Id</param>
        /// <returns></returns>
        Task RemoveConnectionId(string userName, string connectionId);

        /// <summary>
        /// 移除指定用户的缓存
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        Task RemoveUser(string userName);
    }
}