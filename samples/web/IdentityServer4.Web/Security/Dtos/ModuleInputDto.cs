// -----------------------------------------------------------------------
//  <copyright file="ModuleInputDto.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Mapping;
using Hybrid.Zero.Security.Dtos;
using Hybrid.Web.Security.Entities;

using System;

namespace Hybrid.Web.Security.Dtos
{
    /// <summary>
    /// 输入DTO：模块信息
    /// </summary>
    [MapTo(typeof(Module))]
    public class ModuleInputDto : ModuleInputDtoBase<Guid>
    { }
}