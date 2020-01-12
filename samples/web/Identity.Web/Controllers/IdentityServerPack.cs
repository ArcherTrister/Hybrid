
using IdentityServer4;
using IdentityServer4.Configuration;

using Liuliu.Demo.Identity.Entities;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OSharp.Core.Options;
using OSharp.Exceptions;
using OSharp.Web.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Liuliu.Demo.IdentityServer
{
    /// <summary>
    /// 身份认证模块，此模块必须在MVC模块之前启动
    /// </summary>
    [Description("身份认证模块")]
    public class IdentityServerPack : IdentityServerPackBase<User, Guid, Role, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 0;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            //services.AddScoped<IIdentityContract, IdentityService>();

            return base.AddServices(services);
        }

        /// <summary>
        /// 重写以实现<see cref="IdentityOptions"/>的配置
        /// </summary>
        /// <returns></returns>
        protected override Action<IdentityOptions> IdentityOptionsAction()
        {
            return options =>
            {
                //登录
                options.SignIn.RequireConfirmedEmail = false;
                //密码
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                //用户
                options.User.RequireUniqueEmail = false;
                //锁定
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            };
        }

        /// <summary>
        /// 重写以实现<see cref="IdentityServerOptions"/>的配置
        /// </summary>
        /// <returns></returns>
        protected override Action<IdentityServerOptions> IdentityServerOptionsAction()
        {
            return options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            };
        }

        /// <summary>
        /// 重写以实现<see cref="CookieAuthenticationOptions"/>的配置
        /// </summary>
        /// <returns></returns>
        protected override Action<CookieAuthenticationOptions> CookieOptionsAction()
        {
            return options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "osharp.identity";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.LoginPath = "/#/identity/login";
            };
        }

        /// <summary>
        /// 添加Authentication服务
        /// </summary>
        /// <param name="services">服务集合</param>
        protected override void AddAuthentication(IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();

            AuthenticationBuilder authenticationBuilder =  services.AddAuthentication("Bearer")
                    .AddLocalApi(options =>
                    {
                        options.ExpectedScope = "api1";
                    });
            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", options =>
            //    {
            //        options.Authority = "http://localhost:50020";
            //        options.RequireHttpsMetadata = false;
            //        options.Audience = "api1";
            //    });

            // OAuth2
            // https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
            IConfigurationSection section = configuration.GetSection("OSharp:OAuth2");
            IDictionary<string, OAuth2Options> dict = section.Get<Dictionary<string, OAuth2Options>>();
            if (dict == null)
            {
                return;
            }
            foreach (KeyValuePair<string, OAuth2Options> pair in dict)
            {
                OAuth2Options value = pair.Value;
                if (!value.Enabled)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(value.ClientId))
                {
                    throw new OsharpException($"配置文件中OSharp:OAuth2配置的{pair.Key}节点的ClientId不能为空");
                }
                if (string.IsNullOrEmpty(value.ClientSecret))
                {
                    throw new OsharpException($"配置文件中OSharp:OAuth2配置的{pair.Key}节点的ClientSecret不能为空");
                }

                switch (pair.Key)
                {
                    //case "QQ":
                    //    authenticationBuilder.AddQQ(opts =>
                    //    {
                    //        opts.ClientId = value.ClientId;
                    //        opts.ClientSecret = value.ClientSecret;
                    //    });
                    //    break;
                    case "Microsoft":
                        authenticationBuilder.AddMicrosoftAccount(opts =>
                        {
                            opts.ClientId = value.ClientId;
                            opts.ClientSecret = value.ClientSecret;
                        });
                        break;
                    //case "GitHub":
                    //    authenticationBuilder.AddGitHub(opts =>
                    //    {
                    //        opts.ClientId = value.ClientId;
                    //        opts.ClientSecret = value.ClientSecret;
                    //    });
                    //    break;
                    case "Google":
                        authenticationBuilder.AddGoogle(opts =>
                        {
                            opts.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                            // register your IdentityServer with Google at https://console.developers.google.com
                            // enable the Google+ API
                            // set the redirect URI to http://localhost:5000/signin-google
                            opts.ClientId = value.ClientId;
                            opts.ClientSecret = value.ClientSecret;
                        });
                        break;
                }
            }
        }

        /// <summary>
        /// 重写以实现 AddIdentity 之后的构建逻辑
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override IdentityBuilder AddIdentityBuild(IdentityBuilder builder)
        {
            //https：/www.myget.org/F/archertrister/api/v3/index.json
            //如需要昵称唯一，启用下面这个验证码
            //builder.AddUserValidator<UserNickNameValidator<User, int>>();
            builder.AddDefaultUI();
            return builder.AddDefaultTokenProviders();
        }

        /// <summary>
        /// 重写以实现 AddIdentityServer4 之后的构建逻辑
        /// </summary>
        /// <param name="builder"></param>
        protected override IIdentityServerBuilder AddIdentityServerBuilder(IIdentityServerBuilder builder)
        {
            // 对客户端、资源和CORS设置的配置存储支持

            return builder.AddDeveloperSigningCredential()
                   .AddInMemoryIdentityResources(Config.GetIdentityResources())
                   .AddInMemoryApiResources(Config.GetApis())
                   .AddInMemoryClients(Config.GetClients());

            //return builder.AddConfigurationStore(options =>
            //         {
            //             options.ConfigureDbContext = builder =>
            //                 builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            //                     sql => sql.MigrationsAssembly("IdentitySample3"));
            //         })
            //        // 对授权授予、同意和令牌的操作存储支持(刷新和引用)
            //        .AddOperationalStore(options =>
            //        {
            //            options.ConfigureDbContext = builder =>
            //                builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            //                    sql => sql.MigrationsAssembly("IdentitySample3"));

            //            // 指示是否将自动清除数据库中的陈旧项。默认情况是false
            //            options.EnableTokenCleanup = true;
            //            // 令牌清理间隔(以秒为单位)。默认为3600(1小时)
            //            options.TokenCleanupInterval = 30;
            //        });
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public override void UsePack(IApplicationBuilder app)
        {
            app.UseIdentityServer();
#if NETCOREAPP3_0
            app.UseAuthorization();
#endif
            IsEnabled = true;
        }
    }
}