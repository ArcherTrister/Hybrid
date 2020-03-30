// -----------------------------------------------------------------------
//  <copyright file="ModuleOutputDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;
using Hybrid.Mapping;

using LeXun.Demo.Authorization.Entities;

namespace LeXun.Demo.Authorization.Dtos
{
    /// <summary>
    /// 输入DTO:模块信息
    /// </summary>
    [MapFrom(typeof(Module))]
    public class ModuleOutputDto : IOutputDto
    {
        /// <summary>
        /// 获取或设置 模块编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 模块代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置 节点内排序码
        /// </summary>
        public double OrderCode { get; set; }

        /// <summary>
        /// 获取或设置 父模块编号
        /// </summary>
        public int? ParentId { get; set; }
    }
}