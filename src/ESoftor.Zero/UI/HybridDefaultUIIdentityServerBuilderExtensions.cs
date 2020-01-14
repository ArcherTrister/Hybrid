using ESoftor.Permission.UI;

using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HybridDefaultUIIdentityServerBuilderExtensions
    {
        public static void AddHybridDefaultUI<TUser>(this IIdentityServerBuilder builder)
            where TUser : class
        {
            var classes = Assembly.GetEntryAssembly().DefinedTypes;

            var models = classes.Where(a => a.IsClass && a.IsPublic && a.IsAbstract && a.IsDefined(typeof(HybridDefaultUIAttribute)));

            builder.Services.AddMvc()
                .AddMvcOptions(o =>
                {
                    o.Conventions.Add(new HybridApplicationModelConvention());
                    o.Conventions.Add(new HybridControllerModelConvention());
                })
                .ConfigureApplicationPartManager(c =>
                {
                    c.ApplicationParts.Add(new HybridControllerApplicationPart(models, typeof(TUser)));
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
