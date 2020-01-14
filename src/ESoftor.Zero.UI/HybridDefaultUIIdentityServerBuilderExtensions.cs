using ESoftor.Dependency;
using ESoftor.Zero.UI;

using Microsoft.AspNetCore.Mvc.Razor;

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


            IHybridDefaultUIAttributeTypeFinder moduleTypeFinder =
    builder.Services.GetOrAddTypeFinder<IHybridDefaultUIAttributeTypeFinder>(assemblyFinder => new HybridDefaultUIAttributeTypeFinder(assemblyFinder));

            builder.Services.ConfigureOptions(typeof(HybridDefaultUIConfigureOptions));

            builder.Services.Configure<RazorViewEngineOptions>(options => {
                options.ViewLocationExpanders.Add(new HybridViewLocationExpander());
            });


            IMvcBuilder mvcBuilder = builder.Services.AddMvc()
                .AddMvcOptions(o =>
                {
                    o.Conventions.Add(new HybridApplicationModelConvention());
                    o.Conventions.Add(new HybridControllerModelConvention());
                })
                .ConfigureApplicationPartManager(c =>
                {
                    c.ApplicationParts.Add(new HybridControllerApplicationPart(moduleTypeFinder.GetTypeInfos(), typeof(TUser)));
                    c.FeatureProviders.Add(new HybridControllerFeatureProvider());
                });
#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif

            return builder;
        }
    }
}
