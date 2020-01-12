// -----------------------------------------------------------------------
//  <copyright file="IAccessClaimsProvider.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using System.Security.Claims;
using System.Threading.Tasks;

namespace ESoftor.Permission.Identity
{
    /// <summary>
    /// 定义AccessToken的用户Claims提供器
    /// </summary>
    public interface IAccessClaimsProvider
    {
        /// <summary>
        /// 创建用户标识
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Claim[]> CreateClaims(string userId);
    }
}