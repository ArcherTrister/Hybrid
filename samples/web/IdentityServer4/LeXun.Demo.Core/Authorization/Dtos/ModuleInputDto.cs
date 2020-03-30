// -----------------------------------------------------------------------
//  <copyright file="ModuleInputDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Dtos;
using Hybrid.Mapping;

using LeXun.Demo.Authorization.Entities;

namespace LeXun.Demo.Authorization.Dtos
{
    /// <summary>
    /// 输入DTO：模块信息
    /// </summary>
    [MapTo(typeof(Module))]
    public class ModuleInputDto : ModuleInputDtoBase<int>
    { }
}