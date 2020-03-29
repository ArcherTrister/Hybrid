// -----------------------------------------------------------------------
//  <copyright file="HybridOptions.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-03 0:57</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Configuration;
using Hybrid.Net.Mail.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace Hybrid.Core.Options
{
    /// <summary>
    /// Hybrid框架配置选项信息
    /// </summary>
    public class HybridOptions
    {
        /// <summary>
        /// 初始化一个<see cref="HybridOptions"/>类型的新实例
        /// </summary>
        public HybridOptions()
        {
            DbContexts = new ConcurrentDictionary<string, HybridDbContextOptions>(StringComparer.OrdinalIgnoreCase);
            OAuth2S = new ConcurrentDictionary<string, OAuth2Options>();
            EmailSender = new EmailSenderConfiguration();
            Quartz = new QuartzConfiguration();
            Ids = new IdentityServerConfiguration();
        }

        /// <summary>
        /// 获取或设置 邮件发送选项
        /// </summary>
        public EmailSenderConfiguration EmailSender { get; set; }

        /// <summary>
        /// 获取或设置 Quartz选项
        /// </summary>
        public QuartzConfiguration Quartz { get; set; }

        /// <summary>
        /// 获取或设置 IdentityServer4身份认证选项
        /// </summary>
        public IdentityServerConfiguration Ids { get; set; }

        /// <summary>
        /// 获取 数据上下文配置信息
        /// </summary>
        public IDictionary<string, HybridDbContextOptions> DbContexts { get; }

        /// <summary>
        /// 获取 第三方OAuth2登录配置信息
        /// </summary>
        public IDictionary<string, OAuth2Options> OAuth2S { get; }

        /// <summary>
        /// 获取或设置 邮件发送选项
        /// </summary>
        public MailSenderOptions MailSender { get; set; }

        /// <summary>
        /// 获取或设置 JWT身份认证选项
        /// </summary>
        public JwtOptions Jwt { get; set; }

        /// <summary>
        /// 获取或设置 Redis选项
        /// </summary>
        public RedisOptions Redis { get; set; }

        /// <summary>
        /// 获取或设置 Swagger选项
        /// </summary>
        public SwaggerOptions Swagger { get; set; }

        /// <summary>
        /// 获取或设置 Http传输加密选项
        /// </summary>
        public HttpEncryptOptions HttpEncrypt { get; set; }

        /// <summary>
        /// 获取指定上下文类和指定数据库类型的上下文配置信息
        /// </summary>
        public HybridDbContextOptions GetDbContextOptions(Type dbContextType)
        {
            return DbContexts.Values.SingleOrDefault(m => m.DbContextType == dbContextType);
        }
    }
}