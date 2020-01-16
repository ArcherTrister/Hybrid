// -----------------------------------------------------------------------
//  <copyright file="OnlineUser.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Identity.JwtBearer;
using ESoftor.RealTime;

using System.Collections.Generic;

namespace ESoftor.Zero.Identity.RealTime
{
    public class OnlineUser : OnlineUserBase
    {
        /// <summary>
        /// 获取或设置 客户端刷新Token
        /// </summary>
        public IDictionary<string, RefreshToken> RefreshTokens { get; set; }
    }
}
