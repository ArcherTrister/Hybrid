﻿// -----------------------------------------------------------------------
//  <copyright file="FunctionInputDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-11-15 17:25</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Functions;
using Hybrid.Mapping;

namespace Hybrid.Authorization.Dtos
{
    /// <summary>
    /// 输入DTO：功能信息
    /// </summary>
    [MapTo(typeof(Function))]
    public class FunctionInputDto : FunctionInputDtoBase
    { }
}