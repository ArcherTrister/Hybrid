﻿// -----------------------------------------------------------------------
//  <copyright file="AppSettingsManager.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-03-19 22:49</last-date>
// -----------------------------------------------------------------------

using Hybrid.Extensions;

using Microsoft.Extensions.Configuration;

using System.IO;

namespace Hybrid.Core.Options
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    public static class AppSettingsManager
    {
        private static IConfiguration _configuration;

        static AppSettingsManager()
        {
            BuildConfiguration();
        }

        private static void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false).AddJsonFile("appsettings.Development.json", true);
            _configuration = builder.Build();
        }

        /// <summary>
        /// 读取指定节点信息
        /// </summary>
        /// <param name="key">节点名称，多节点以:分隔</param>
        public static string Get(string key)
        {
            return _configuration[key];
        }

        /// <summary>
        /// 读取指定节点信息
        /// </summary>
        public static T Get<T>(string key)
        {
            string json = Get(key);
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            return json.FromJsonString<T>();
        }
    }
}