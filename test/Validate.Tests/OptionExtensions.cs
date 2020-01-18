using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Validate.Tests
{
    public static class OptionExtensions
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        public static void ValidateByDataAnnotation(object instance, string sectionName)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(instance);
            var valid = Validator.TryValidateObject(instance, context, validationResults);
            if (valid) return;

            var msg = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
            throw new Exception($"Invalid configuration of section '{sectionName}':\n{msg}");
        }

        public static OptionsBuilder<TOptions> ValidateByDataAnnotation<TOptions>(
            this OptionsBuilder<TOptions> builder,
            string sectionName)
            where TOptions : class, IEnabled
        {
            return builder.PostConfigure(x => {
                if (x.Enabled)
                {
                    ValidateByDataAnnotation(x, sectionName);
                }
            });
        }

        public static IServiceCollection ConfigureAndValidate<TOptions>(
            this IServiceCollection services,
            string sectionName,
            IConfiguration configuration)
            where TOptions : class, IEnabled
        {
            var section = configuration.GetSection(sectionName);

            services
                .AddOptions<TOptions>()
                .Bind(section)
                .ValidateByDataAnnotation(sectionName);

            return services;
        }
    }
}
