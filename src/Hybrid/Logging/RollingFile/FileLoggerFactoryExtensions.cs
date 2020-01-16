// -----------------------------------------------------------------------
//  <copyright file="FileLoggerFactoryExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 21:17</last-date>
// -----------------------------------------------------------------------

using Hybrid.Logging.RollingFile;

using Microsoft.Extensions.Logging;

using System;

//power by https://github.com/andrewlock/NetEscapades.Extensions.Logging
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for adding the <see cref="FileLoggerProvider" /> to the <see cref="ILoggingBuilder" />
    /// </summary>
    public static class FileLoggerFactoryExtensions
    {
        /// <summary>
        /// Adds a file logger named 'File' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
            return builder;
        }

        /// <summary>
        /// Adds a file logger named 'File' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <param name="filename">Sets the filename prefix to use for log files</param>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filename)
        {
            builder.AddFile(options =>
            {
                options.FileName = filename;
            });
            return builder;
        }

        /// <summary>
        /// Adds a file logger named 'File' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <param name="configure">Configure an instance of the <see cref="FileLoggerOptions" /> to set logging options</param>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, Action<FileLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }
            builder.AddFile();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}