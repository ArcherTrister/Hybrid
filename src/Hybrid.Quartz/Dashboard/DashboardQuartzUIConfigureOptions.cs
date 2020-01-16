﻿using Hybrid.Extensions;

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
        public DashboardQuartzUiConfigureOptions(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public IHostingEnvironment Environment { get; }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            //if (string.IsNullOrEmpty(name))
            //{
            //    throw new ArgumentNullException(nameof(name));
            //}

            options = options ?? throw new ArgumentNullException(nameof(options));

            // Basic initialization in case the options weren't initialized by any other component
            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && Environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? Environment.WebRootFileProvider;

            // Add our provider
            //var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "Dashboard\\Content");
            var filesProvider = new ManifestEmbeddedFileProvider(typeof(DashboardQuartzUiConfigureOptions).GetAssembly(), "Dashboard/Content");
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }
    }
}