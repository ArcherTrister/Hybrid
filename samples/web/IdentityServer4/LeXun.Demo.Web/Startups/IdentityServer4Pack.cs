using Hybrid.AutoMapper;
using Hybrid.Core.Configuration;
using Hybrid.Core.Options;
using Hybrid.Data;
using Hybrid.Entity;
using Hybrid.Zero.IdentityServer4;
using Hybrid.Zero.IdentityServer4.Stores;
using IdentityServer4.Stores;
using LeXun.Demo.Identity;
using LeXun.Demo.Identity.Dtos;
using LeXun.Demo.Identity.Entities;
using LeXun.Demo.Identity.Events;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace LeXun.Demo.Web.Startups
{
    /// <summary>
    /// 身份认证模块，此模块必须在MVC模块之前启动
    /// </summary>
    [Description("身份认证模块")]
    public class IdentityServer4Pack : IdentityServer4PackBase<UserStore, RoleStore, User, int, UserClaim, int, Role, int>
    {
        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<IIdentityContract, IdentityService>();
            services.AddSingleton<IAutoMapperConfiguration, AutoMapperConfiguration>();
            services.AddSingleton<ISeedDataInitializer, RoleSeedDataInitializer>();

            services.AddEventHandler<LoginLoginLogEventHandler>();
            services.AddEventHandler<LogoutLoginLogEventHandler>();

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

        ///// <summary>
        ///// 重写以实现<see cref="IdentityServerOptions"/>的配置
        ///// </summary>
        ///// <returns></returns>
        //protected override Action<IdentityServerOptions> IdentityServerOptionsAction()
        //{
        //    return options =>
        //    {
        //        //Authentication
        //        ////设置用于交互用户的主机所混杂的cookie创作方案。如果未设置，则该方案将从主机的默认身份验证方案中推断。当主机中使用AddPolicyScheme作为默认方案时，通常使用此设置。
        //        //options.Authentication.CookieAuthenticationScheme = IdentityServerConstants.DefaultCheckSessionCookieName;
        //        //用于检查会话终结点的cookie的名称。默认为idsrv.session
        //        //options.Authentication.CheckSessionCookieName = "";
        //        //身份验证cookie生存期(只有在使用IdghtyServer提供的cookie处理程序时才有效)。
        //        options.Authentication.CookieLifetime = TimeSpan.FromMinutes(720);
        //        //指定Cookie是否应该是滑动的(只有在使用IdghtyServer提供的Cookie处理程序时才有效)。
        //        options.Authentication.CookieSlidingExpiration = true;
        //        //指示用户是否必须通过身份验证才能接受结束会话终结点的参数。默认为false。
        //        //options.Authentication.RequireAuthenticatedUserForSignOutMessage = false;
        //        //如果设置，将需要在结束会话回调端点上发出帧-src csp报头，该端点将iframes呈现给客户端以进行前端通道签名通知。默认为true。
        //        //options.Authentication.RequireCspFrameSrcForSignout = true;

        //        options.Events.RaiseErrorEvents = true;
        //        options.Events.RaiseFailureEvents = true;
        //        options.Events.RaiseInformationEvents = true;
        //        options.Events.RaiseSuccessEvents = true;

        //        options.UserInteraction = new UserInteractionOptions
        //        {
        //            LoginUrl = "/Account/Login",//【必备】登录地址
        //            LogoutUrl = "/Account/Logout",//【必备】退出地址
        //            ConsentUrl = "/Consent/Index",//【必备】允许授权同意页面地址
        //            ErrorUrl = "/Home/Error", //【必备】错误页面地址
        //            LoginReturnUrlParameter = "ReturnUrl",//【必备】设置传递给登录页面的返回URL参数的名称。默认为returnUrl
        //            LogoutIdParameter = "logoutId", //【必备】设置传递给注销页面的注销消息ID参数的名称。缺省为logoutId
        //            ConsentReturnUrlParameter = "ReturnUrl", //【必备】设置传递给同意页面的返回URL参数的名称。默认为returnUrl
        //            ErrorIdParameter = "errorId", //【必备】设置传递给错误页面的错误消息ID参数的名称。缺省为errorId
        //            CustomRedirectReturnUrlParameter = "ReturnUrl", //【必备】设置从授权端点传递给自定义重定向的返回URL参数的名称。默认为returnUrl
        //            CookieMessageThreshold = 5 //【必备】由于浏览器对Cookie的大小有限制，设置Cookies数量的限制，有效的保证了浏览器打开多个选项卡，一旦超出了Cookies限制就会清除以前的Cookies值
        //        };
        //    };
        //}

        /// <summary>
        /// 添加Authentication服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="idsOptions"></param>
        /// <param name="configuration"></param>
        protected override void AddAuthentication(IServiceCollection services, IdentityServerConfiguration idsOptions, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            AuthenticationBuilder authenticationBuilder = services.AddAuthentication();

            //AuthenticationBuilder authenticationBuilder = services.AddAuthentication(options =>
            //{
            //    //options.DefaultScheme = "cookie";
            //    options.DefaultChallengeScheme = "oidc";
            //});

            //authenticationBuilder.AddCookie(options =>
            //{
            //    options.Cookie.Name = "HybridCookie";
            //    //options.Events.OnSigningOut = async e => { await e.HttpContext.RevokeUserRefreshTokenAsync(); };
            //});
            //authenticationBuilder.AddOpenIdConnect("oidc", options =>
            //                 {
            //                     options.Authority = idsOptions.Authority;

            //                     options.ClientId = "";
            //                     options.ClientSecret = "secret";

            //                     // code flow + PKCE (PKCE is turned on by default)
            //                     options.ResponseType = "code";
            //                     options.UsePkce = true;

            //                     options.Scope.Clear();
            //                     options.Scope.Add("openid");
            //                     options.Scope.Add("profile");
            //                     options.Scope.Add("email");
            //                     options.Scope.Add("offline_access");
            //                     options.Scope.Add(idsOptions.Audience);

            //                     // not mapped by default
            //                     options.ClaimActions.MapJsonKey("website", "website");

            //                     // keeps id_token smaller
            //                     options.GetClaimsFromUserInfoEndpoint = true;
            //                     options.SaveTokens = true;

            //                     options.TokenValidationParameters = new TokenValidationParameters
            //                     {
            //                         NameClaimType = "name",
            //                         RoleClaimType = "role"
            //                     };
            //                 });

            if (idsOptions.IsLocalApi)
            {
                // 1.如果在本项目中使用webapi则添加，并且在UseModule中不能使用app.UseAuthentication
                // 2.在webapi上添加[Authorize(AuthenticationSchemes = IdentityServerConstants.LocalApi.AuthenticationScheme)]标记
                // 3.在本框架中使用HybridConsts.LocalApi.AuthenticationScheme
                authenticationBuilder.AddLocalApi(HybridConstants.LocalApi.AuthenticationScheme,
                    options =>
                    {
                        options.ExpectedScope = HybridConstants.LocalApi.ScopeName;
                        options.SaveToken = true;
                    });

                // OAuth2
                IConfigurationSection section = configuration.GetSection("Hybrid:OAuth2");
                IDictionary<string, OAuth2Options> dict = section.Get<Dictionary<string, OAuth2Options>>();
                if (dict == null)
                {
                    return;
                }
                foreach (KeyValuePair<string, OAuth2Options> pair in dict)
                {
                    OAuth2Options value = pair.Value;
                    //https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
                    switch (pair.Key)
                    {
                        case "QQ":
                            authenticationBuilder.AddQQ(opts =>
                            {
                                opts.ClientId = value.ClientId;
                                opts.ClientSecret = value.ClientSecret;
                            });
                            break;
                        //case "Microsoft":
                        //    authenticationBuilder.AddMicrosoftAccount(opts =>
                        //    {
                        //        opts.ClientId = value.ClientId;
                        //        opts.ClientSecret = value.ClientSecret;
                        //    });
                        //    break;
                        //case "GitHub":
                        //    authenticationBuilder.AddGitHub(opts =>
                        //    {
                        //        opts.ClientId = value.ClientId;
                        //        opts.ClientSecret = value.ClientSecret;
                        //    });
                        //    break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                // TODO: IdentityServer
                //// IdentityServer
                //services.AddAuthentication(Configuration["IdentityService:DefaultScheme"])
                //    //.AddIdentityServerAuthentication(options =>
                //    //{
                //    //    options.Authority = Configuration["IdentityService:Uri"];
                //    //    options.RequireHttpsMetadata = Convert.ToBoolean(Configuration["IdentityService:UseHttps"]);
                //    //    options.ApiName = serviceName;
                //    //})
                //    .AddJwtBearer(Configuration["IdentityService:DefaultScheme"], options =>
                //    {
                //        options.Authority = Configuration["IdentityService:Uri"];
                //        options.RequireHttpsMetadata = Convert.ToBoolean(Configuration["IdentityService:UseHttps"]);
                //        options.Audience = serviceName;
                //        options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(1);//验证token超时时间频率
                //    options.TokenValidationParameters.RequireExpirationTime = true;
                //    });
            }

            //// adds user and client access token management
            //services.AddAccessTokenManagement(options =>
            //{
            //    // client config is inferred from OpenID Connect settings
            //    // if you want to specify scopes explicitly, do it here, otherwise the scope parameter will not be sent
            //    options.Client.Scope = HybridConsts.LocalApi.ScopeName;
            //})
            //    .ConfigureBackchannelHttpClient()
            //        .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(new[]
            //        {
            //            TimeSpan.FromSeconds(1),
            //            TimeSpan.FromSeconds(2),
            //            TimeSpan.FromSeconds(3)
            //        }));
        }

        /// <summary>
        /// 重写以实现 AddIdentityServer 之后的构建逻辑，如果非授权服务器，此处配置无效
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override IIdentityServerBuilder AddIdentityServerBuild(IIdentityServerBuilder builder, IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();
            string basePath = PlatformServices.Default.Application.ApplicationBasePath;
            //makecert -r -pe -n "C=CN,CN=domain,O=LX,OU=IT,ST=KM,L=YN,E=ARCHERTRISTER@OUTLOOK.COM" -b 04/01/2020 -e 04/01/2100 -sv idsrv4.pvk idsrv4.cer
            //cert2spc idsrv4.cer idsrv4.spc
            //pvk2pfx -spc idsrv4.spc -pvk idsrv4.pvk -pi MooYu -pfx idsrv4.pfx -f
            builder.AddSigningCredential(new X509Certificate2(fileName: Path.Combine(basePath,
                    configuration["Hybrid:Ids:CerPath"]), password: configuration["Hybrid:Ids:CerPwd"]))
                //.AddCustomAuthorizeRequestValidator<>()
                //.AddCustomTokenRequestValidator<>()
                //.AddValidators()
                .AddHybridDefaultUI<User, int>()
                .AddHybridTokenCreationService<CustomTokenCreationService>()
                .AddHybridCustomTokenValidator<CustomTokenValidator>();

            //builder.AddConfigurationStoreCache();
            //AddInMemoryStores
            builder.AddInMemoryIdentityResources(IdentityServer4Config.GetIdentityResources());
            builder.AddInMemoryApiResources(IdentityServer4Config.GetApis());
            builder.AddInMemoryClients(IdentityServer4Config.GetClients());

            return builder;

            ////持久化
            ////IConfiguration configuration = services.GetConfiguration();
            ////Add-Migration InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
            ////Add-Migration InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
            ////IConfigurationSection section = configuration.GetSection("Hybrid:Idxxxxxx");
            //string entryAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            //return builder
            //        // this adds the config data from DB (clients, resources)
            //        .AddConfigurationStore(options =>
            //        {
            //            options.ConfigureDbContext = build =>
            //                build.UseSqlServer("",
            //                    sql => sql.MigrationsAssembly(entryAssemblyName));
            //        })
            //        // this adds the operational data from DB (codes, tokens, consents)
            //        .AddOperationalStore(options =>
            //        {
            //            options.ConfigureDbContext = build =>
            //                build.UseSqlServer("",
            //                    sql => sql.MigrationsAssembly(entryAssemblyName));

            //            // this enables automatic token cleanup. this is optional.
            //            options.EnableTokenCleanup = true;
            //            options.TokenCleanupInterval = 30; // interval in seconds
            //        });
        }
    }
}
