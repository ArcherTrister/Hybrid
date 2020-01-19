using Hybrid.Core.Options;
using Hybrid.Domain.Entities;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hybrid.Extensions
{
    public static class OptionExtensions
    {
        private static void ValidateByDataAnnotation(object instance, string sectionName)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(instance);
            var valid = Validator.TryValidateObject(instance, context, validationResults);
            if (valid) return;

            var msg = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
            throw new Exception($"Invalid configuration of section '{sectionName}':\n{msg}");
        }

    //    public static OptionsBuilder<TOptions> ValidateDataAnnotation<TOptions>(
    //this OptionsBuilder<TOptions> builder,
    //string sectionName)
    //where TOptions : class
    //    {
    //        return builder.PostConfigure(x =>
    //        {
    //            ValidateByDataAnnotation(x, sectionName);
    //        });
    //    }

    //    public static IServiceCollection ConfigureAndValidate<TOptions>(
    //        this IServiceCollection services,
    //        string sectionName,
    //        IConfiguration configuration)
    //        where TOptions : class
    //    {
    //        var section = configuration.GetSection(sectionName);

    //        services
    //            .AddOptions<TOptions>()
    //            .Bind(section)
    //            .ValidateDataAnnotation(sectionName);

    //        return services;
    //    }

        public static OptionsBuilder<TOptions> ValidateDataAnnotationByEnabled<TOptions>(
            this OptionsBuilder<TOptions> builder,
            string sectionName)
            where TOptions : class, IEnabled
        {
            return builder.PostConfigure(x =>
            {
                if (x.IsEnabled)
                {
                    ValidateByDataAnnotation(x, sectionName);
                }
            });
        }

        public static IServiceCollection ConfigureAndValidateByEnable<TOptions>(
            this IServiceCollection services,
            string sectionName,
            IConfiguration configuration)
            where TOptions : class, IEnabled
        {
            var section = configuration.GetSection(sectionName);

            services
                .AddOptions<TOptions>()
                .Bind(section)
                .ValidateDataAnnotationByEnabled(sectionName);

            return services;
        }

        public static OptionsBuilder<TOptions> ValidateDataAnnotationHybridOption<TOptions>(this OptionsBuilder<TOptions> builder)
            where TOptions : HybridOptions
        {
            return builder.PostConfigure(x =>
            {
                //if (x.Auditing.IsEnabled)
                //{
                //    ValidateByDataAnnotation(x.Auditing, x.Auditing.GetType().Name);
                //}
                foreach (var item in x.DbContexts)
                {
                    ValidateByDataAnnotation(item.Value, item.Value.GetType().Name);
                }
                if (x.EmailSender.IsEnabled)
                {
                    ValidateByDataAnnotation(x.EmailSender, x.EmailSender.GetType().Name);
                }
                foreach (var item in x.OAuth2S)
                {
                    ValidateByDataAnnotation(item.Value, item.Value.GetType().Name);
                }
            });
        }

        public static IServiceCollection ConfigureAndValidateHybridOption<TOptions>(this IServiceCollection services, IConfiguration configuration)
            where TOptions : HybridOptions
        {
            string sectionName = "Hybrid";
            var section = configuration.GetSection(sectionName);

            services
                .AddOptions<TOptions>()
                .Bind(section)
                .ValidateDataAnnotationHybridOption();

            return services;
        }
    }
}