// -----------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="cn.lxking">
//      Copyright (c) 2014 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-13 20:48:22</last-date>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Linq;
using System.Reflection;

using Test.Web.Conventions;
using Test.Web.Providers;
using Test.Web.Services;

using WebApplication123;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUI<TUser>(
            this IServiceCollection services)
            where TUser : class
        {
            services.TryAddScoped<IDoService<TUser>, DoService<TUser>>();

            var classes = Assembly.GetEntryAssembly().DefinedTypes;
            //var classes = Assembly.DefinedTypes.ToList();
            var models = classes.Where(a=>a.IsClass && a.IsPublic && a.IsAbstract && a.IsDefined(typeof(UIAttribute)));

            services.AddMvc()
                .AddMvcOptions(o =>
                {
                    o.Conventions.Add(new MyApplicationModelConvention());
                    o.Conventions.Add(new GenericControllerModelConvention());
                })
                .ConfigureApplicationPartManager(c =>
                {
                    c.ApplicationParts.Add(new WebApplication123.GenericControllerApplicationPart(models, typeof(TUser)));
                    c.FeatureProviders.Add(new MyControllerFeatureProvider());
                    //c.FeatureProviders.Add(new GenericControllerFeatureProvider<TUser>(models));
                });

            //services.Configure<RazorViewEngineOptions>(options =>
            //{
            //    if (options.AreaViewLocationFormats.Select(p => p.Equals("/Views/{1}/{0}.cshtml", StringComparison.OrdinalIgnoreCase)).Any())
            //    {
            //        options.AreaViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
            //    }
            //    //options.AreaViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
            //    ////options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            //    //options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            //});
        }
    }
}
