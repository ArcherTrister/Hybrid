// -----------------------------------------------------------------------
//  <copyright file="HybridDefaultUIConfigureOptions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

using System;

namespace Hybrid.Zero.IdentityServer4.Quickstart
{
    internal class HybridIdentityServerUIConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        public HybridIdentityServerUIConfigureOptions(IWebHostEnvironment environment)
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
            var filesProvider = new ManifestEmbeddedFileProvider(typeof(HybridIdentityServerUIConfigureOptions).GetAssembly(), "Quickstart/Content");
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }
    }
}