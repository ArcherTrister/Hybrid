// -----------------------------------------------------------------------
//  <copyright file="UserNameUserIdProvider.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2019-01-04 20:34</last-date>
// -----------------------------------------------------------------------

using System.Security.Claims;

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
            return connection.User?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}