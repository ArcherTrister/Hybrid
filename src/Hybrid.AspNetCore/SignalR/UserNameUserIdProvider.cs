// -----------------------------------------------------------------------
//  <copyright file="UserNameUserIdProvider.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-01-04 20:34</last-date>
// -----------------------------------------------------------------------

using Hybrid.Security.Claims;

using Microsoft.AspNetCore.SignalR;

namespace Hybrid.AspNetCore.SignalR
{
    /// <summary>
    /// 用户名用户标识提供者
    /// </summary>
    public class UserNameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(HybridClaimTypes.UserName)?.Value;
        }
    }
}