// -----------------------------------------------------------------------
//  <copyright file="UserSetModuleDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-08 12:21</last-date>
// -----------------------------------------------------------------------

namespace LeXun.Demo.Authorization.Dtos
{
    /// <summary>
    /// 用户设置模块DTO
    /// </summary>
    public class UserSetModuleDto
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置 要设置的模块编号
        /// </summary>
        public int[] ModuleIds { get; set; }
    }
}