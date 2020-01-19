using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Validate.Tests.Options;

using Xunit;

namespace Validate.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [Fact]
        public void CanValidateDataAnnotations()
        {
            var services = new ServiceCollection();
            services.AddOptions<AnnotatedOptions>()
                .Configure(o =>
                {
                    o.StringLength = "111111";
                    o.IntRange = 10;
                    o.Custom = "nowhere";
                })
                .ValidateDataAnnotations();

            var sp = services.BuildServiceProvider();

            var error = Assert.ThrowsException<OptionsValidationException>(() =>
                sp.GetRequiredService<IOptionsMonitor<AnnotatedOptions>>().CurrentValue);
        }

        [Fact]
        public void ValidateTest()
        {
            var services = new ServiceCollection();
            var sp = services.BuildServiceProvider();
            IConfiguration Configuration = OptionExtensions.InitConfiguration();
            services.AddOptions();
            services
                .AddOptions<KafkaOptions>()
                .Bind(Configuration.GetSection("Kafka"))
                .Validate(option =>

                    // return a bool result:
                    Validator.TryValidateObject(
                        option,
                        new ValidationContext(option),
                        new List<ValidationResult>(),
                        validateAllProperties: true),

                    // generic error message:
                    "Kafka section invalid"
                );

            var error = Assert.ThrowsException<OptionsValidationException>(() =>
    sp.GetRequiredService<IOptionsMonitor<AnnotatedOptions>>().CurrentValue);
        }

        [Fact]
        public void PostConfigureTest()
        {
            var services = new ServiceCollection();
            var sp = services.BuildServiceProvider();
            IConfiguration Configuration = OptionExtensions.InitConfiguration();
            services.AddOptions();
            services
                .AddOptions<KafkaOptions>()
                .Bind(Configuration.GetSection("Kafka"))
                .PostConfigure(x =>
                {
                    var validationResults = new List<ValidationResult>();
                    var context = new ValidationContext(x);
                    var valid = Validator.TryValidateObject(x, context, validationResults);
                    if (valid) return;

                    var msg = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                    throw new Exception($"Invalid configuration of section 'Kafka':\n{msg}");
                });

            var error = Assert.ThrowsException<OptionsValidationException>(() =>
    sp.GetRequiredService<IOptionsMonitor<AnnotatedOptions>>().CurrentValue);
        }

        [Fact]
        public void CustomTest()
        {
            var services = new ServiceCollection();
            var sp = services.BuildServiceProvider();
            IConfiguration Configuration = OptionExtensions.InitConfiguration();
            services.AddOptions();
            services.ConfigureAndValidate<KafkaOptions>("Kafka", Configuration);

            var error = Assert.ThrowsException<OptionsValidationException>(() =>
    sp.GetRequiredService<IOptionsMonitor<AnnotatedOptions>>().CurrentValue);
        }

        [Fact]
        public void Validate()
        {
        
        }
    }
}