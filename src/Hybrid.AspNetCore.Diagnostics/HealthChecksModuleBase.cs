// -----------------------------------------------------------------------
//  <copyright file="HealthChecksModuleBase.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-10-05 19:21</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;
using Hybrid.Core.Options;
using Hybrid.Domain.EntityFramework;
using Hybrid.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Newtonsoft.Json;

using System;
using System.Linq;

namespace Hybrid.AspNetCore.Diagnostics
{
    /// <summary>
    /// 程序健康检查模块
    /// </summary>
    [DependsOnModules(typeof(AspNetCoreModule))]
    public abstract class HealthChecksModuleBase : AspHybridModule
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Application;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 1;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();
            bool enabled = configuration["Hybrid:HealthChecks:Enabled"].CastTo(false);
            if (!enabled)
            {
                return services;
            }

            IHealthChecksBuilder builder = services.AddHealthChecks();
            BuildHealthChecks(builder, configuration);
            return services;
        }

        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        public override void UseModule(IApplicationBuilder app)
        {
            IServiceProvider provider = app.ApplicationServices;
            IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
            bool enabled = configuration["Hybrid:HealthChecks:Enabled"].CastTo(false);
            if (!enabled)
            {
                return;
            }

            string url = configuration["Hybrid:HealthChecks:Url"] ?? "/health";
            HealthCheckOptions options = GetHealthCheckOptions(provider);
            if (options != null)
            {
                app.UseHealthChecks(url, options);
            }
            else
            {
                app.UseHealthChecks(url);
            }

            IsEnabled = true;
        }

        /// <summary>
        /// 建立HealthChecks服务
        /// </summary>
        /// <param name="builder">HealthChecks服务创建者</param>
        /// <param name="configuration">应用程序配置</param>
        /// <returns></returns>
        protected virtual IHealthChecksBuilder BuildHealthChecks(IHealthChecksBuilder builder, IConfiguration configuration)
        {
            IConfigurationSection section = configuration.GetSection("Hybrid");
            HybridOptions options = section.Get<HybridOptions>();

            //system
            long providerMemory = configuration["Hybrid:HealthChecks:PrivateMemory"].CastTo(1000_000_000L);
            long virtualMemorySize = configuration["Hybrid:HealthChecks:VirtualMemorySize"].CastTo(1000_000_000L);
            long workingSet = configuration["Hybrid:HealthChecks:WorkingSet"].CastTo(1000_000_000L);
            builder.AddPrivateMemoryHealthCheck(providerMemory); //最大私有内存
            builder.AddVirtualMemorySizeHealthCheck(virtualMemorySize); //最大虚拟内存
            builder.AddWorkingSetHealthCheck(workingSet); //最大工作内存

            //数据库
            foreach (var pair in options.DbContexts.OrderBy(m => m.Value.DatabaseType))
            {
                string connectionString = pair.Value.ConnectionString;
                switch (pair.Value.DatabaseType)
                {
                    case DatabaseType.SqlServer:
                        builder.AddSqlServer(connectionString, null, pair.Key);
                        break;

                    case DatabaseType.Sqlite:
                        builder.AddSqlite(connectionString, name: pair.Key);
                        break;

                    case DatabaseType.MySql:
                        builder.AddMySql(connectionString, pair.Key);
                        break;

                    case DatabaseType.PostgreSql:
                        builder.AddNpgSql(connectionString, name: pair.Key);
                        break;

                    case DatabaseType.Oracle:
                        builder.AddOracle(connectionString, name: pair.Key);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException($"HybridOptions中 {pair.Value.DatabaseType} 不受支持");
                }
            }

            //SMTP
            if (options.EmailSender != null)
            {
                var smtp = options.EmailSender;
                builder.AddSmtpHealthCheck(smtpOptions =>
                {
                    smtpOptions.Host = smtp.Host;
                    smtpOptions.LoginWith(smtp.UserName, smtp.Password);
                });
            }

            //Redis
            if (options.Redis != null && options.Redis.IsEnabled)
            {
                var redis = options.Redis;
                builder.AddRedis(redis.Configuration);
            }

            //Hangfire
            if (configuration["Hybrid:Hangfire:Enabled"].CastTo(false))
            {
                builder.AddHangfire(hangfireOptions =>
                {
                    hangfireOptions.MinimumAvailableServers = 1;
                });
            }

            return builder;
        }

        /// <summary>
        /// 重写以创建HealthCheckOptions
        /// </summary>
        protected virtual HealthCheckOptions GetHealthCheckOptions(IServiceProvider provider)
        {
            return new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonConvert.SerializeObject(
                        new
                        {
                            status = report.Status.ToString(),
                            errors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status), e.Value.Description })
                        });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }
            };
        }
    }
}