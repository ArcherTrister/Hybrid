﻿using Hybrid.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

using System;

namespace Hybrid.Quartz.Dashboard
{
    internal class DashboardQuartzUiConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        public DashboardQuartzUiConfigureOptions(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public IWebHostEnvironment Environment { get; }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));

            // Basic initialization in case the options weren't initialized by any other component
            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && Environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? Environment.WebRootFileProvider;

            // Add our provider
            //var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "Quickstart\\Content");
            var filesProvider = new ManifestEmbeddedFileProvider(typeof(DashboardQuartzUiConfigureOptions).GetAssembly(), "Dashboard/Content");
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }
    }
}