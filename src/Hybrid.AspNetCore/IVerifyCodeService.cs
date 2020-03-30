// -----------------------------------------------------------------------
//  <copyright file="IVerifyCodeService.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-19 17:15</last-date>
// -----------------------------------------------------------------------

using System.Drawing;

namespace Hybrid.AspNetCore
{
    /// <summary>
    /// 定义验证码处理功能
    /// </summary>
    public interface IVerifyCodeService
    {
        /// <summary>
        /// 校验验证码有效性
        /// </summary>
        /// <param name="code">要校验的验证码</param>
        /// <param name="id">验证码编号</param>
        /// <param name="removeIfSuccess">验证成功时是否移除</param>
        /// <returns></returns>
        bool CheckCode(string code, string id, bool removeIfSuccess = true);

        /// <summary>
        /// 设置验证码到Session中
        /// </summary>
        void SetCode(string code, out string id);

        /// <summary>
        /// 将图片序列化成字符串
        /// </summary>
        string GetImageString(Image image, string id);
    }
}