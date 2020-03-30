// -----------------------------------------------------------------------
//  <copyright file="AutoMapperConfiguration.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-04 0:24</last-date>
// -----------------------------------------------------------------------

using AutoMapper.Configuration;

using Hybrid.AutoMapper;

using LeXun.Demo.Identity.Entities;

namespace LeXun.Demo.Identity.Dtos
{
    /// <summary>
    /// DTO对象映射配置
    /// </summary>
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