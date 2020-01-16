﻿// -----------------------------------------------------------------------
//  <copyright file="VerifyCodeService.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using Hybrid.Dependency;
using Hybrid.Extensions;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Hybrid.AspNetCore.Services
{
    /// <summary>
    /// 验证码处理服务
    /// </summary>
    [Dependency(ServiceLifetime.Singleton, TryAdd = true)]
    public class VerifyCodeService : IVerifyCodeService
    {
        private const string Separator = "#$#";
        private readonly IDistributedCache _cache;

        /// <summary>
        /// 初始化一个<see cref="VerifyCodeService"/>类型的新实例
        /// </summary>
        public VerifyCodeService(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 校验验证码有效性
        /// </summary>
        /// <param name="code">要校验的验证码</param>
        /// <param name="id">验证码编号</param>
        /// <param name="removeIfSuccess">验证成功时是否移除</param>
        /// <returns></returns>
        public bool CheckCode(string code, string id, bool removeIfSuccess = true)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }

            string key = $"{ESoftorConstants.VerifyCodeKeyPrefix}_{id}";
            bool flag = code.Equals(_cache.GetString(key), StringComparison.OrdinalIgnoreCase);
            if (removeIfSuccess && flag)
            {
                _cache.Remove(key);
            }

            return flag;
        }

        /// <summary>
        /// 设置验证码到Session中
        /// </summary>
        public void SetCode(string code, out string id)
        {
            id = Guid.NewGuid().ToString("N");
            string key = $"{ESoftorConstants.VerifyCodeKeyPrefix}_{id}";
            const int seconds = 60 * 3;
            _cache.SetString(key, code, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(seconds) });
        }

        /// <summary>
        /// 将图片序列化成字符串
        /// </summary>
        public string GetImageString(Image image, string id)
        {
            Check.NotNull(image, nameof(image));
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                byte[] bytes = ms.ToArray();
                string str = $"data:image/png;base64,{bytes.ToBase64String()}{Separator}{id}";
                return str.ToBase64String();
            }
        }
    }
}