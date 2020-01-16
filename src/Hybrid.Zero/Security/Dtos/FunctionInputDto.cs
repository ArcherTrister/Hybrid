// -----------------------------------------------------------------------
//  <copyright file="FunctionInputDto.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Functions;
using Hybrid.Mapping;

namespace Hybrid.Zero.Security.Dtos
{
    /// <summary>
    /// 输入DTO：功能信息
    /// </summary>
    [MapTo(typeof(Function))]
    public class FunctionInputDto : FunctionInputDtoBase
    { }
}