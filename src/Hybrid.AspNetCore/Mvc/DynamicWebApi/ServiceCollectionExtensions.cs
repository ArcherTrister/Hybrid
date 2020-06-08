using Hybrid.AspNetCore.DynamicWebApi;

using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Add Dynamic WebApi
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Dynamic WebApi to Container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicWebApi(this IServiceCollection services, DynamicWebApiOptions options)
        {
            if (options == null)
            {
                throw new ArgumentException(nameof(options));
            }

            options.Valid();

            DynamicWebApiConsts.DefaultAreaName = options.DefaultAreaName;
            DynamicWebApiConsts.DefaultHttpVerb = options.DefaultHttpVerb;
            DynamicWebApiConsts.DefaultApiPreFix = options.DefaultApiPrefix;
            DynamicWebApiConsts.IsUseRestFul = options.IsUseRestFul;
            DynamicWebApiConsts.ControllerPostfixes = options.RemoveControllerPostfixes;
            DynamicWebApiConsts.ActionPostfixes = options.RemoveActionPostfixes;
            //Ignore MVC Form Binding types.
            DynamicWebApiConsts.FormBodyBindingIgnoredTypes = new List<Type>() { typeof(IFormFile) };
            DynamicWebApiConsts.GetRestFulActionName = options.GetRestFulActionName;
            //Specifies the dynamic webapi options for the assembly.
            DynamicWebApiConsts.AssemblyDynamicWebApiOptions = options.AssemblyDynamicWebApiOptions;

            return services;
        }

        public static IServiceCollection AddDynamicWebApi(this IServiceCollection services)
        {
            return AddDynamicWebApi(services, new DynamicWebApiOptions());
        }

        public static IServiceCollection AddDynamicWebApi(this IServiceCollection services, Action<DynamicWebApiOptions> optionsAction)
        {
            var dynamicWebApiOptions = new DynamicWebApiOptions();

            optionsAction?.Invoke(dynamicWebApiOptions);

            return AddDynamicWebApi(services, dynamicWebApiOptions);
        }
    }
}