// -----------------------------------------------------------------------
//  <copyright file="UserNickNameValidator" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-12 14:20:33</last-date>
// -----------------------------------------------------------------------

using ESoftor.Zero.Identity.Extensions;

using Microsoft.AspNetCore.Identity;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESoftor.Zero.Identity
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