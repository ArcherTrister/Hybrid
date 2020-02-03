// -----------------------------------------------------------------------
//  <copyright file="IJwtBearerService.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-02 1:34</last-date>
// -----------------------------------------------------------------------

using Hybrid.Zero.Identity;
using System.Threading.Tasks;

namespace Hybrid.Identity.JwtBearer
{
    /// <summary>
    /// 定义JwtBearer服务，负责JwtToken的创建，验证，刷新等业务
    /// </summary>
    public interface IJwtBearerService
    {
        /// <summary>
        /// 创建指定用户的JwtToken信息
        /// </summary>
        /// <param name="userId">用户编号的字符串</param>
        /// <param name="userName">用户名的字符串</param>
        /// <param name="refreshToken">刷新Token模型</param>
        /// <returns>JwtToken信息</returns>
        Task<JsonWebToken> CreateToken(string userId, string userName, RequestClientType clientType = RequestClientType.Browser);

        /// <summary>
        /// 使用RefreshToken获取新的JwtToken信息
        /// </summary>
        /// <param name="refreshToken">刷新Token字符串</param>
        /// <returns>JwtToken信息</returns>
        Task<JsonWebToken> RefreshToken(string refreshToken);
    }
}