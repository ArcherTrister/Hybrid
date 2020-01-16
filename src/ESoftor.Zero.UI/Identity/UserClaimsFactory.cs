﻿// -----------------------------------------------------------------------
//  <copyright file="UserClaimsFactory" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-12 17:17:13</last-date>
// -----------------------------------------------------------------------

using ESoftor.Zero.Identity;
using ESoftor.Security.Claims;

using Microsoft.AspNetCore.Identity;

using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ESoftor.Zero.IdentityServer4.Identity
{
    internal class UserClaimsFactory<TUser, TUserKey> : IUserClaimsPrincipalFactory<TUser>
            where TUser : UserBase<TUserKey>
            where TUserKey : IEquatable<TUserKey>
    {
        private readonly Decorator<IUserClaimsPrincipalFactory<TUser>> _inner;
        private UserManager<TUser> _userManager;

        public UserClaimsFactory(Decorator<IUserClaimsPrincipalFactory<TUser>> inner, UserManager<TUser> userManager)
        {
            _inner = inner;
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var principal = await _inner.Instance.CreateAsync(user);
            var identity = principal.Identities.First();

            var findUser = await _userManager.FindByIdAsync(user.Id.ToString());

            if (!identity.HasClaim(x => x.Type == HybridClaimTypes.Subject))
            {
                identity.AddClaim(new Claim(HybridClaimTypes.Subject, user.Id.ToString()));
            }

            if (!identity.HasClaim(x => x.Type == HybridClaimTypes.UserId))
            {
                identity.AddClaim(new Claim(HybridClaimTypes.UserId, findUser.Id.ToString()));
            }

            PropertyInfo property = typeof(TUser).GetProperty("Id");
            if (property != null)
            {
                identity.AddClaim(new Claim(HybridClaimTypes.UserIdTypeName, property.PropertyType.FullName));
            }

            var username = findUser.UserName;
            var usernameClaim = identity.FindFirst(claim => claim.Type == _userManager.Options.ClaimsIdentity.UserNameClaimType && claim.Value == username);
            if (usernameClaim != null)
            {
                identity.RemoveClaim(usernameClaim);
                identity.AddClaim(new Claim(HybridClaimTypes.PreferredUserName, username));
            }

            if (!identity.HasClaim(x => x.Type == HybridClaimTypes.UserName))
            {
                identity.AddClaim(new Claim(HybridClaimTypes.UserName, username));
            }

            if (!identity.HasClaim(x => x.Type == HybridClaimTypes.NickName))
            {
                var nickName = findUser.NickName;
                identity.AddClaim(new Claim(HybridClaimTypes.NickName, nickName));
            }

            if (_userManager.SupportsUserEmail)
            {
                var email = findUser.Email;
                if (!string.IsNullOrWhiteSpace(email))
                {
                    identity.AddClaims(new[]
                    {
                        new Claim(HybridClaimTypes.Email, email),
                        new Claim(HybridClaimTypes.EmailVerified,
                            findUser.EmailConfirmed
                            ? "true" : "false", ClaimValueTypes.Boolean)
                    });
                }
            }

            if (_userManager.SupportsUserPhoneNumber)
            {
                var phoneNumber = findUser.PhoneNumber;
                if (!string.IsNullOrWhiteSpace(phoneNumber))
                {
                    identity.AddClaims(new[]
                    {
                        new Claim(HybridClaimTypes.MobilePhone, phoneNumber),
                        new Claim(HybridClaimTypes.PhoneNumberVerified,
                            findUser.PhoneNumberConfirmed
                            ? "true" : "false", ClaimValueTypes.Boolean)
                    });
                }
            }
            return principal;
        }
    }
}