using ESoftor.Dependency;
using ESoftor.Zero.UI;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HybridDefaultUIIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddHybridDefaultUI<TUser>(this IIdentityServerBuilder builder)
            where TUser : class
        {
            //var hybridDefaultUIOptions = new HybridDefaultUIOptions();

            //_dashboardQuartzOptions?.Invoke(hybridDefaultUIOptions);

            //services.AddSingleton(hybridDefaultUIOptions);

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

            IHybridDefaultUIAttributeTypeFinder moduleTypeFinder =
            builder.Services.GetOrAddTypeFinder<IHybridDefaultUIAttributeTypeFinder>(assemblyFinder => new HybridDefaultUIAttributeTypeFinder(assemblyFinder));

            builder.Services.ConfigureOptions(typeof(HybridDefaultUIConfigureOptions));

            builder.Services.AddMvc()
                .AddMvcOptions(o =>
                {
                    o.Conventions.Add(new HybridApplicationModelConvention());
                    //o.Conventions.Add(new HybridControllerModelConvention());
                })
                .ConfigureApplicationPartManager(c =>
                {
                    c.ApplicationParts.Add(new HybridControllerApplicationPart(moduleTypeFinder.GetTypeInfos(), typeof(TUser)));
                });

            return builder;
        }
    }
}
