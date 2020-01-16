// -----------------------------------------------------------------------
//  <copyright file="IdentityExtensions.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using Hybrid.Extensions;

using Microsoft.AspNetCore.Identity;

using System.Linq;

namespace Hybrid.Zero.Identity.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// 将<see cref="IdentityResult"/>转化为<see cref="OperationResult"/>
        /// </summary>
        public static OperationResult ToOperationResult(this IdentityResult identityResult)
        {
            return identityResult.Succeeded
                ? new OperationResult(OperationResultType.Success)
                : new OperationResult(OperationResultType.Error,
                    identityResult.Errors.Select(m => m.Description).ExpandAndToString());
        }

        /// <summary>
        /// 将<see cref="IdentityResult"/>转化为<see cref="OperationResult{TUser}"/>
        /// </summary>
        public static OperationResult<TUser> ToOperationResult<TUser>(this IdentityResult identityResult, TUser user)
        {
            return identityResult.Succeeded
                ? new OperationResult<TUser>(OperationResultType.Success, "Success", user)
                : new OperationResult<TUser>(OperationResultType.Error,
                    identityResult.Errors.Select(m => m.Description).ExpandAndToString());
        }

        /// <summary>
        /// 快速创建错误信息的IdentityResult
        /// </summary>
        public static IdentityResult Failed(this IdentityResult identityResult, params string[] errors)
        {
            var identityErrors = identityResult.Errors;
            identityErrors = identityErrors.Union(errors.Select(m => new IdentityError() { Description = m }));
            return IdentityResult.Failed(identityErrors.ToArray());
        }
    }
}