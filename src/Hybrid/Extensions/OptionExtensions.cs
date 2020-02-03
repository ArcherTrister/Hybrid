using Hybrid.Core.Options;
using Hybrid.Domain.Entities;
using Hybrid.Exceptions;
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
            throw new HybridException($"Invalid configuration of section '{sectionName}':\n{msg}");
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
                //    ValidateByDataAnnotation(x.Auditing, nameof(x.Auditing));
                //}
                if (x.DbContexts.Any())
                {
                    ValidateByDataAnnotation(x.DbContexts, nameof(x.DbContexts));
                }
                if (x.EmailSender.IsEnabled)
                {
                    ValidateByDataAnnotation(x.EmailSender, nameof(x.EmailSender));
                }
                if (x.OAuth2S.Any())
                {
                    ValidateByDataAnnotation(x.OAuth2S, nameof(x.OAuth2S));
                }
                if (x.Quartz.IsEnabled)
                {
                    ValidateByDataAnnotation(x.Quartz, nameof(x.Quartz));
                }
                if (x.Ids.IsEnabled && x.Jwt.IsEnabled)
                {
                    throw new HybridException("不能同时启用Ids服务和Jwt服务");
                }
                if (x.Ids.IsEnabled && !x.Ids.IsLocalApi)
                {
                    ValidateByDataAnnotation(x.Ids, nameof(x.Ids));
                }
                if (x.Jwt.IsEnabled)
                {
                    ValidateByDataAnnotation(x.Jwt, nameof(x.Jwt));
                }
                if (x.Redis.IsEnabled)
                {
                    ValidateByDataAnnotation(x.Redis, nameof(x.Redis));
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