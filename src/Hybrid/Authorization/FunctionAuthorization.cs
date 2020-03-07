// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthorization.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-05-11 1:05</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Security
{
    /// <summary>
    /// 功能权限验证类
    /// </summary>
    public class FunctionAuthorization : FunctionAuthorizationBase
    {
        /// <summary>
        /// 初始化一个<see cref="FunctionAuthorizationBase"/>类型的新实例
        /// </summary>
        public FunctionAuthorization(IFunctionAuthCache functionAuthCache)
            : base(functionAuthCache)
        { }
    }
}