using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Test.Web.Models;

namespace Test.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddOptions<HybridOptions>()
            //    .Configure(o =>
            //    {
            //        o.StringLength = "111111";
            //        o.IntRange = 10;
            //        o.Custom = "nowhere";
            //    })
            //    .ValidateDataAnnotations();

            //services.AddOptions<HybridOptions>()
            //    //.Configure(Configuration.GetSection("Hybrid"))
            //    .Bind(Configuration.GetSection("Hybrid"))
            //    .ValidateDataAnnotations();

            //services.AddOptions<CustomConfig>()
            //   .Bind(Configuration.GetSection("CustomConfig"))

            //   .Validate(c =>
            //   {
            //       if (string.IsNullOrEmpty(c.Setting2))
            //       {
            //           return true;
            //       }
            //       return false;
            //   })
            //                  .ValidateDataAnnotations();

            //.Validate(c =>
            //{
            //    if (c.Setting2 != default)
            //    {
            //        return c.Setting3 != default;
            //    }

            //    return true;
            //}, "Setting 3 is required when Setting2 is present");

            //#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            //            var sp = services.BuildServiceProvider();
            //#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            //            var a = sp.GetRequiredService<IOptionsMonitor<CustomConfig>>().CurrentValue;

            //var error = Assert.Throws<OptionsValidationException>(() =>
            //    sp.GetRequiredService<IOptionsMonitor<CustomConfig>>().CurrentValue);

            //ValidateFailure<CustomConfig>(error, Options.DefaultName, 1,
            //    "DataAnnotation validation failed for members Required " +
            //        "with the error 'The Required field is required.'.",
            //    "DataAnnotation validation failed for members StringLength " +
            //        "with the error 'Too long.'.",
            //    "DataAnnotation validation failed for members IntRange " +
            //        "with the error 'Out of range.'.");

            services.AddOptions();

            //services
            //    .AddOptions<KafkaOptions>()
            //    .Bind(Configuration.GetSection("Kafka"))
            //    .Validate(option =>

            //        // return a bool result:
            //        Validator.TryValidateObject(
            //            option,
            //            new ValidationContext(option),
            //            new List<ValidationResult>(),
            //            validateAllProperties: true),

            //        // generic error message:
            //        "Kafka section invalid"
            //    );

            //        services
            //.AddOptions<KafkaOptions>()
            //.Bind(Configuration.GetSection("Kafka"))
            //.PostConfigure(x =>
            //{
            //    var validationResults = new List<ValidationResult>();
            //    var context = new ValidationContext(x);
            //    var valid = Validator.TryValidateObject(x, context, validationResults);
            //    if (valid) return;

            //    var msg = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
            //    throw new Exception($"Invalid configuration of section 'Kafka':\n{msg}");
            //});

            services.ConfigureAndValidate<KafkaOptions>("Kafka", Configuration);
#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            var sp = services.BuildServiceProvider();
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            var a = sp.GetRequiredService<IOptionsMonitor<KafkaOptions>>().CurrentValue;

            services.AddControllersWithViews();

            //services.AddUI<User>();

            services.AddUI<User, Guid>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}