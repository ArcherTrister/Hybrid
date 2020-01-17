// -----------------------------------------------------------------------
//  <copyright file="UserSetModuleDto.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-08 12:21</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.Web.Security.Dtos
{
    /// <summary>
    /// 用户设置模块DTO
    /// </summary>
    public class UserSetModuleDto
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 获取或设置 要设置的模块编号
        /// </summary>
        public Guid[] ModuleIds { get; set; }
    }
}