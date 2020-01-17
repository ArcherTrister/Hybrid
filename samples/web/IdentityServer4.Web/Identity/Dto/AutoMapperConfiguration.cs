// -----------------------------------------------------------------------
//  <copyright file="AutoMapperConfiguration.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-04 0:24</last-date>
// -----------------------------------------------------------------------

using AutoMapper.Configuration;

using Hybrid.AutoMapper;
using Hybrid.Dependency;
using Hybrid.Web.Identity.Entity;

using Microsoft.Extensions.DependencyInjection;

namespace Hybrid.Web.Identity.Dtos
{
    /// <summary>
    /// DTO对象映射配置
    /// </summary>
    [Dependency(ServiceLifetime.Singleton)]
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        /// <summary>
        /// 创建对象映射
        /// </summary>
        /// <param name="mapper">映射配置表达</param>
        public void CreateMaps(MapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Role, RoleNode>().ForMember(rn => rn.RoleId, opt => opt.MapFrom(r => r.Id))
                .ForMember(rn => rn.RoleName, opt => opt.MapFrom(r => r.Name));
        }
    }
}