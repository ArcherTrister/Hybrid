using ESoftor.Web.Identity.Stores;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OSharp.AspNetCore;
using OSharp.Core.Packs;
using OSharp.EventBuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSharp.Web.Identity
{
    /// <summary>
    /// Identity And IdentityServer4身份论证模块基类
    /// </summary>
    [DependsOnPacks(typeof(EventBusPack), typeof(AspNetCorePack))]
    public abstract class IdentityServerPackBase<TUser, TUserKey, TRole, TRoleKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim> : AspOsharpPack
        where TUser : UserBase<TUserKey>
        where TRole : RoleBase<TRoleKey>
        where TUserClaim : UserClaimBase<TUserKey>, new()
        where TUserRole : UserRoleBase<TUserKey, TRoleKey>, new()
        where TUserLogin : UserLoginBase<TUserKey>, new()
        where TUserToken : UserTokenBase<TUserKey>, new()
        where TRoleClaim : RoleClaimBase<TRoleKey>, new()
        where TUserKey : IEquatable<TUserKey>
        where TRoleKey : IEquatable<TRoleKey>
    {
        public override PackLevel Level => PackLevel.Application;

        //public override int Order => base.Order;

        public override IServiceCollection AddServices(IServiceCollection services)
        {
            ////在线用户缓存
            //services.TryAddScoped<IOnlineUserProvider, OnlineUserProvider<TUser, TUserKey, TRole, TRoleKey>>();

            Action<IdentityOptions> identityOptionsAction = IdentityOptionsAction();

            IdentityBuilder identityBuilder = services.AddIdentity<TUser, TRole>(identityOptionsAction)
                    //.AddEntityFrameworkStores<TDbContext>();
                    .AddUserStore<UserStore<TUser, TUserKey, TUserClaim, TUserLogin, TUserToken, TRole, TRoleKey, TUserRole>>()
                    .AddRoleStore<RoleStore<TRole, TRoleKey, TRoleClaim>>();

            //services.Replace(new ServiceDescriptor(typeof(IdentityErrorDescriber), typeof(IdentityErrorDescriberZhHans), ServiceLifetime.Scoped));

            AddIdentityBuild(identityBuilder);

            Action<IdentityServerOptions> identityServerOptionsAction = IdentityServerOptionsAction();

            IIdentityServerBuilder identityServerBuilder = services.AddIdentityServer(identityServerOptionsAction)
                    .AddAspNetIdentity<TUser>();

            AddIdentityServerBuilder(identityServerBuilder);

            Action<CookieAuthenticationOptions> cookieOptionsAction = CookieOptionsAction();
            if (cookieOptionsAction != null)
            {
                services.ConfigureApplicationCookie(cookieOptionsAction);
            }

            AddAuthentication(services);


            return services;
        }

        /// <summary>
        /// 重写以实现<see cref="IdentityOptions"/>的配置
        /// </summary>
        /// <returns></returns>
        protected virtual Action<IdentityOptions> IdentityOptionsAction()
        {
            return null;
        }

        /// <summary>
        /// 重写以实现<see cref="IdentityServerOptions"/>的配置
        /// </summary>
        /// <example>
        /// return options =>
        /// {
        ///    options.Events.RaiseErrorEvents = true;
        ///    options.Events.RaiseInformationEvents = true;
        ///    options.Events.RaiseFailureEvents = true;
        ///    options.Events.RaiseSuccessEvents = true;
        /// };
        /// </example>
        /// <returns></returns>
        protected virtual Action<IdentityServerOptions> IdentityServerOptionsAction()
        {
            return null;
        }

        /// <summary>
        /// 重写以实现 AddIdentity 之后的构建逻辑
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected virtual IdentityBuilder AddIdentityBuild(IdentityBuilder builder)
        {
            return builder;
        }

        /// <summary>
        /// 重写以实现 AddIdentityServer 之后的构建逻辑
        /// </summary>
        /// <param name="builder"></param>
        /// <example>
        /// .AddInMemoryIdentityResources(Config.GetIdentityResources())
        /// .AddInMemoryApiResources(Config.GetApis())
        /// .AddInMemoryClients(Config.GetClients());
        /// </example>
        protected virtual IIdentityServerBuilder AddIdentityServerBuilder(IIdentityServerBuilder builder)
        {
            return builder;
        }

        /// <summary>
        /// 重写以实现<see cref="CookieAuthenticationOptions"/>的配置
        /// </summary>
        /// <returns></returns>
        protected virtual Action<CookieAuthenticationOptions> CookieOptionsAction()
        {
            return null;
        }

        /// <summary>
        /// 添加Authentication服务
        /// </summary>
        /// <param name="services">服务集合</param>
        protected virtual void AddAuthentication(IServiceCollection services)
        { }
    }
}
