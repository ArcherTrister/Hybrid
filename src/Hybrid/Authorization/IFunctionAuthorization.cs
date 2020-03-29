// -----------------------------------------------------------------------
//  <copyright file="IFunctionAuthorization.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-05-10 19:56</last-date>
// -----------------------------------------------------------------------

using System.Security.Principal;

using Hybrid.Authorization.Functions;


namespace Hybrid.Authorization
{
    /// <summary>
    /// 定义功能权限验证
    /// </summary>
    public interface IFunctionAuthorization
    {
        /// <summary>
        /// 检查指定用户是否有执行指定功能的权限
        /// </summary>
        /// <param name="function">要检查的功能</param>
        /// <param name="principal">在线用户信息</param>
        /// <returns>功能权限检查结果</returns>
        AuthorizationResult Authorize(IFunction function, IPrincipal principal);

        /// <summary>
        /// 获取功能权限检查通过的角色
        /// </summary>
        /// <param name="function">要检查的功能</param>
        /// <param name="principal">在线用户信息</param>
        /// <returns>通过的角色</returns>
        string[] GetOkRoles(IFunction function, IPrincipal principal);
    }
}