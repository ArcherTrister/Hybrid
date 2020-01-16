// -----------------------------------------------------------------------
//  <copyright file="JwtHandler.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Options;
using Hybrid.Exceptions;

using Microsoft.IdentityModel.Tokens;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hybrid.Zero.Identity
{
    /// <summary>
    /// Jwt辅助操作类
    /// </summary>
    public class JwtHelper
    {
        /// <summary>
        /// 生成JwtToken
        /// </summary>
        public static string CreateToken(Claim[] claims, ESoftorOptions options)
        {
            JwtOptions jwtOptions = options.Jwt;
            string secret = jwtOptions.Secret;
            if (secret == null)
            {
                throw new ESoftorException("创建JwtToken时Secret为空");
            }
            SecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            DateTime now = DateTime.UtcNow;
            double days = Math.Abs(jwtOptions.AccessExpireMins) > 0 ? Math.Abs(jwtOptions.AccessExpireMins) : 7;
            DateTime expires = now.AddDays(days);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = jwtOptions.Audience,
                Issuer = jwtOptions.Issuer,
                SigningCredentials = credentials,
                NotBefore = now,
                IssuedAt = now,
                Expires = expires
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}