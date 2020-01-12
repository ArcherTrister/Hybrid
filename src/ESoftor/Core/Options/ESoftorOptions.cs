// -----------------------------------------------------------------------
//  <copyright file="ESoftorOptions.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-03 0:57</last-date>
// -----------------------------------------------------------------------

using ESoftor.Audits;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ESoftor.Core.Options
{
    /// <summary>
    /// ESoftor框架配置选项信息
    /// </summary>
    public class ESoftorOptions
    {
        /// <summary>
        /// 初始化一个<see cref="ESoftorOptions"/>类型的新实例
        /// </summary>
        public ESoftorOptions()
        {
            DbContexts = new ConcurrentDictionary<string, ESoftorDbContextOptions>(StringComparer.OrdinalIgnoreCase);
            OAuth2S = new ConcurrentDictionary<string, OAuth2Options>();
            AuditingConfiguration = new AuditingConfiguration();
        }

        /// <summary>
        /// 获取 审计配置信息
        /// </summary>
        public AuditingConfiguration AuditingConfiguration { get; set; }

        /// <summary>
        /// 获取 数据上下文配置信息
        /// </summary>
        public IDictionary<string, ESoftorDbContextOptions> DbContexts { get; }

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
        /// 获取指定上下文类和指定数据库类型的上下文配置信息
        /// </summary>
        public ESoftorDbContextOptions GetDbContextOptions(Type dbContextType)
        {
            return DbContexts.Values.SingleOrDefault(m => m.DbContextType == dbContextType);
        }
    }
}