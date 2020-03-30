// -----------------------------------------------------------------------
//  <copyright file="UserNickNameValidator.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-05-01 20:11</last-date>
// -----------------------------------------------------------------------

using Hybrid.Identity.Entities;

using Microsoft.AspNetCore.Identity;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Identity
{
    /// <summary>
    /// 用户昵称验证器
    /// </summary>
    public class UserNickNameValidator<TUser, TUserKey> : IUserValidator<TUser>
        where TUser : UserBase<TUserKey>
        where TUserKey : struct, IEquatable<TUserKey>
    {
        /// <summary>
        /// 验证用户昵称合法性
        /// </summary>
        public virtual Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            IdentityResult result = IdentityResult.Success;
            TUser existUser = manager.Users.FirstOrDefault(m => m.NickName == user.NickName);
            if (existUser != null
                && (Equals(user.Id, default(TUserKey)) /*新注册用户，ID为默认值*/
                || !Equals(user.Id, existUser.Id)/*已存在用户不是要编辑的用户*/))
            {
                result = new IdentityResult().Failed($"昵称为“{user.NickName}”的用户已存在，请更换昵称重试");
            }
            return Task.FromResult(result);
        }
    }
}