// -----------------------------------------------------------------------
//  <copyright file="ESoftorOptionsSetup.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-03 12:32</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.EntityFramework;
using Hybrid.Exceptions;
using Hybrid.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using System.Collections.Generic;
using System.Linq;

namespace Hybrid.Core.Options
{
    /// <summary>
    /// Hybrid配置选项创建器
    /// </summary>
    public class ESoftorOptionsSetup : IConfigureOptions<ESoftorOptions>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 初始化一个<see cref="ESoftorOptionsSetup"/>类型的新实例
        /// </summary>
        public ESoftorOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>Invoked to configure a TOptions instance.</summary>
        /// <param name="options">The options instance to configure.</param>
        public void Configure(ESoftorOptions options)
        {
            SetDbContextOptions(options);

            IConfigurationSection section;
            //OAuth2
            section = _configuration.GetSection("Hybrid:OAuth2");
            IDictionary<string, OAuth2Options> dict = section.Get<Dictionary<string, OAuth2Options>>();
            if (dict != null)
            {
                foreach (KeyValuePair<string, OAuth2Options> item in dict)
                {
                    options.OAuth2S.Add(item.Key, item.Value);
                }
            }

            //MailSender
            section = _configuration.GetSection("Hybrid:MailSender");
            MailSenderOptions sender = section.Get<MailSenderOptions>();
            if (sender != null)
            {
                if (sender.Password == null)
                {
                    sender.Password = _configuration["Hybrid:MailSender:Password"];
                }
                options.MailSender = sender;
            }

            //JwtOptions
            section = _configuration.GetSection("Hybrid:Jwt");
            JwtOptions jwt = section.Get<JwtOptions>();
            if (jwt != null)
            {
                if (jwt.Secret == null)
                {
                    jwt.Secret = _configuration["Hybrid:Jwt:Secret"];
                }
                options.Jwt = jwt;
            }

            // RedisOptions
            section = _configuration.GetSection("Hybrid:Redis");
            RedisOptions redis = section.Get<RedisOptions>();
            if (redis != null)
            {
                if (redis.Configuration.IsMissing())
                {
                    throw new ESoftorException("配置文件中Redis节点的Configuration不能为空");
                }
                options.Redis = redis;
            }

            // SwaggerOptions
            section = _configuration.GetSection("Hybrid:Swagger");
            SwaggerOptions swagger = section.Get<SwaggerOptions>();
            if (swagger != null)
            {
                if (swagger.Url.IsMissing())
                {
                    throw new ESoftorException("配置文件中Swagger节点的Url不能为空");
                }
                options.Swagger = swagger;
            }
        }

        /// <summary>
        /// 初始化上下文配置信息，首先以Hybrid配置节点中的为准，
        /// 不存在Hybrid节点，才使用ConnectionStrings的数据连接串来构造SqlServer的配置，
        /// 保证同一上下文类型只有一个配置节点
        /// </summary>
        /// <param name="options"></param>
        private void SetDbContextOptions(ESoftorOptions options)
        {
            IConfigurationSection section = _configuration.GetSection("Hybrid:DbContexts");
            IDictionary<string, ESoftorDbContextOptions> dict = section.Get<Dictionary<string, ESoftorDbContextOptions>>();
            if (dict == null || dict.Count == 0)
            {
                string connectionString = _configuration["ConnectionStrings:DefaultDbContext"];
                if (connectionString == null)
                {
                    return;
                }
                ESoftorDbContextOptions dbContextOptions = new ESoftorDbContextOptions()
                {
                    DbContextTypeName = "Hybrid.EntityFrameworkCore.Defaults.DefaultDbContext,Hybrid.EntityFrameworkCore",
                    ConnectionString = connectionString,
                    DatabaseType = DatabaseType.SqlServer
                };
                options.DbContexts.Add("DefaultDbContext", dbContextOptions);
                return;
            }
            var repeated = dict.Values.GroupBy(m => m.DbContextType).FirstOrDefault(m => m.Count() > 1);
            if (repeated != null)
            {
                throw new ESoftorException($"数据上下文配置中存在多个配置节点指向同一个上下文类型：{repeated.First().DbContextTypeName}");
            }

            foreach (KeyValuePair<string, ESoftorDbContextOptions> pair in dict)
            {
                options.DbContexts.Add(pair.Key, pair.Value);
            }
        }
    }
}