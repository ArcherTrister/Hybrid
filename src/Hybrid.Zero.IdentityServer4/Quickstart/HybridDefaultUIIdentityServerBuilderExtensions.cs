﻿// -----------------------------------------------------------------------
//  <copyright file="HybridDefaultUIIdentityServerBuilderExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Dependency;
using Hybrid.Zero.Identity;
using Hybrid.Zero.IdentityServer4.Quickstart;

using Microsoft.AspNetCore.Mvc.Razor;

using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HybridDefaultUIIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddHybridDefaultUI<TUser, TUserKey>(this IIdentityServerBuilder builder)
            where TUser : UserBase<TUserKey>
            where TUserKey : IEquatable<TUserKey>
        {
            //var hybridDefaultUIOptions = new HybridDefaultUIOptions();

            //_dashboardQuartzOptions?.Invoke(hybridDefaultUIOptions);

            //services.AddSingleton(hybridDefaultUIOptions);


            IHybridDefaultUIAttributeTypeFinder typeFinder =
            builder.Services.GetOrAddTypeFinder<IHybridDefaultUIAttributeTypeFinder>(assemblyFinder => new HybridDefaultUIAttributeTypeFinder(assemblyFinder));

            builder.Services.ConfigureOptions(typeof(HybridDefaultUIConfigureOptions));

            builder.Services.Configure<RazorViewEngineOptions>(options => {
                options.ViewLocationExpanders.Add(new HybridViewLocationExpander());
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
    }
}
