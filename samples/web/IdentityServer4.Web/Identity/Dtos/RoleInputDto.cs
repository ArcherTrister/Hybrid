// -----------------------------------------------------------------------
// <copyright file="RoleInputDto.cs" company="cn.lxking">
//     Copyright © 2019-2020 Hybrid. All rights reserved.
// </copyright>
// <site>https://www.lxking.cn</site>
// <last-editor>ArcherTrister</last-editor>
// <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Mapping;
using Hybrid.Web.Identity.Entities;
using Hybrid.Zero.Identity.Dtos;

using System;

namespace Hybrid.Web.Identity.Dtos
{
    /// <summary>
    /// 输入DTO：角色信息
    /// </summary>
    [MapTo(typeof(Role))]
    public class RoleInputDto : RoleInputDtoBase<Guid>
    { }
}