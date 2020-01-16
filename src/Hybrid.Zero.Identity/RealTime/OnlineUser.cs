// -----------------------------------------------------------------------
//  <copyright file="OnlineUser.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Identity.JwtBearer;
using Hybrid.RealTime;

using System.Collections.Generic;

namespace Hybrid.Zero.Identity.RealTime
{
    public class OnlineUser : OnlineUserBase
    {
        /// <summary>
        /// 获取或设置 客户端刷新Token
        /// </summary>
        public IDictionary<string, RefreshToken> RefreshTokens { get; set; }
    }
}
