// -----------------------------------------------------------------------
//  <copyright file="IdentityServerBuilderExtensions" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-12 17:15:37</last-date>
// -----------------------------------------------------------------------

using Hybrid.Dependency;
using Hybrid.Zero.Identity;
using Hybrid.Zero.IdentityServer4.Identity;
using Hybrid.Zero.IdentityServer4.Quickstart;

using IdentityModel;

using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Validation;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;

using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods to add ASP.NET Identity support to IdentityServer.
    /// </summary>
    public static class IdentityServerBuilderExtensions
    {
        /// <summary>
        /// Configures IdentityServer to use the ASP.NET Identity implementations
        /// of IUserClaimsPrincipalFactory, IResourceOwnerPasswordValidator, and IProfileService.
        /// Also configures some of ASP.NET Identity's options for use with IdentityServer (such as claim types to use
        /// and authenticaiton cookie settings).
        /// </summary>
        /// <typeparam name="TUser">The type of the user.</typeparam>
        /// <typeparam name="TUserKey"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddHybridIdentity<TUser, TUserKey>(this IIdentityServerBuilder builder)
            where TUser : UserBase<TUserKey>
            where TUserKey : IEquatable<TUserKey>
        {
            builder.Services.AddTransientDecorator<IUserClaimsPrincipalFactory<TUser>, UserClaimsFactory<TUser, TUserKey>>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
            });

            builder.Services.Configure<SecurityStampValidatorOptions>(opts =>
            {
                opts.OnRefreshingPrincipal = SecurityStampValidatorCallback.UpdatePrincipal;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.IsEssential = true;
                // we need to disable to allow iframe for authorize requests
                options.Cookie.SameSite = AspNetCore.Http.SameSiteMode.None;
            });

            builder.Services.ConfigureExternalCookie(options =>
            {
                options.Cookie.IsEssential = true;
                // https://github.com/IdentityServer/IdentityServer4/issues/2595
                options.Cookie.SameSite = AspNetCore.Http.SameSiteMode.None;
            });

            builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.TwoFactorRememberMeScheme, options =>
            {
                options.Cookie.IsEssential = true;
            });

            builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.TwoFactorUserIdScheme, options =>
            {
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddAuthentication(options =>
            {
                if (options.DefaultAuthenticateScheme == null &&
                    options.DefaultScheme == IdentityServerConstants.DefaultCookieAuthenticationScheme)
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                }
            });

            builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator<TUser, TUserKey>>();
            builder.AddProfileService<ProfileService<TUser, TUserKey>>();

            return builder;
        }

        /// <summary>
        /// Adds the custom create token service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddHybridTokenCreationService<T>(this IIdentityServerBuilder builder)
           where T : class, ITokenCreationService
        {
            builder.Services.AddTransient<ITokenCreationService, T>();

            return builder;
        }

        /// <summary>
        /// Adds the custom token validator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddHybridCustomTokenValidator<T>(this IIdentityServerBuilder builder)
           where T : class, ICustomTokenValidator
        {
            builder.Services.AddTransient<ICustomTokenValidator, T>();

            return builder;
        }

        public static IIdentityServerBuilder AddHybridDefaultUI<TUser, TUserKey>(this IIdentityServerBuilder builder)
    where TUser : UserBase<TUserKey>
    where TUserKey : IEquatable<TUserKey>
        {
            //var hybridDefaultUIOptions = new HybridDefaultUIOptions();

            //_dashboardQuartzOptions?.Invoke(hybridDefaultUIOptions);

            //services.AddSingleton(hybridDefaultUIOptions);

            IHybridDefaultUIAttributeTypeFinder typeFinder =
            builder.Services.GetOrAddTypeFinder<IHybridDefaultUIAttributeTypeFinder>(assemblyFinder => new HybridDefaultUIAttributeTypeFinder(assemblyFinder));

            builder.Services.ConfigureOptions(typeof(HybridIdentityServerUIConfigureOptions));

            builder.Services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new HybridIdentityServerViewLocationExpander());
            });

            IMvcBuilder mvcBuilder = builder.Services.AddMvc()
                .AddMvcOptions(o =>
                {
                    //o.Conventions.Add(new HybridApplicationModelConvention());
                    o.Conventions.Add(new HybridControllerModelConvention());
                })
                .ConfigureApplicationPartManager(c =>
                {
                    c.ApplicationParts.Add(new HybridControllerApplicationPart(typeFinder.GetTypeInfos(), typeof(TUser), typeof(TUserKey)));
                    c.FeatureProviders.Add(new HybridControllerFeatureProvider());
                });
#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif

            return builder;
        }

        internal static void AddTransientDecorator<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddDecorator<TService>();
            services.AddTransient<TService, TImplementation>();
        }

        internal static void AddDecorator<TService>(this IServiceCollection services)
        {
            var registration = services.LastOrDefault(x => x.ServiceType == typeof(TService));
            if (registration == null)
            {
                throw new InvalidOperationException("Service type: " + typeof(TService).Name + " not registered.");
            }
            if (services.Any(x => x.ServiceType == typeof(Decorator<TService>)))
            {
                throw new InvalidOperationException("Decorator already registered for type: " + typeof(TService).Name + ".");
            }

            services.Remove(registration);

            if (registration.ImplementationInstance != null)
            {
                var type = registration.ImplementationInstance.GetType();
                var innerType = typeof(Decorator<,>).MakeGenericType(typeof(TService), type);
                services.Add(new ServiceDescriptor(typeof(Decorator<TService>), innerType, ServiceLifetime.Transient));
                services.Add(new ServiceDescriptor(type, registration.ImplementationInstance));
            }
            else if (registration.ImplementationFactory != null)
            {
                services.Add(new ServiceDescriptor(typeof(Decorator<TService>), provider =>
                {
                    return new DisposableDecorator<TService>((TService)registration.ImplementationFactory(provider));
                }, registration.Lifetime));
            }
            else
            {
                var type = registration.ImplementationType;
                var innerType = typeof(Decorator<,>).MakeGenericType(typeof(TService), registration.ImplementationType);
                services.Add(new ServiceDescriptor(typeof(Decorator<TService>), innerType, ServiceLifetime.Transient));
                services.Add(new ServiceDescriptor(type, type, registration.Lifetime));
            }
        }
    }
}