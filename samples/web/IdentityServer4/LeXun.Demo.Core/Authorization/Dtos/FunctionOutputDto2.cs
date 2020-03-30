// -----------------------------------------------------------------------
//  <copyright file="FunctionOutputDto2.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Functions;
using Hybrid.Entity;
using Hybrid.Mapping;

using System;

namespace LeXun.Demo.Authorization.Dtos
{
    /// <summary>
    /// 简单功能输出DTO
    /// </summary>
    [MapFrom(typeof(Function))]
    public class FunctionOutputDto2 : IOutputDto
    {
        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 功能名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 访问类型
        /// </summary>
        public FunctionAccessType AccessType { get; set; }

        /// <summary>
        /// 获取或设置 区域名称
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 获取或设置 控制器名称
        /// </summary>
        public string Controller { get; set; }
    }
}