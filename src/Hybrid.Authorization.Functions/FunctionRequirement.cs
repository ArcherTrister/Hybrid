// -----------------------------------------------------------------------
//  <copyright file="FunctionRequirement.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-11 14:07</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;


namespace Hybrid.Authorization
{
    /// <summary>
    /// 功能点授权需求
    /// </summary>
    public class FunctionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Hybrid授权策略名称
        /// </summary>
        public const string HybridPolicy = "HybridPolicy";
    }
}