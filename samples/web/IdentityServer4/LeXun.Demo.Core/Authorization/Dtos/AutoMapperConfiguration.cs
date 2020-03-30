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
using Hybrid.Json;

using LeXun.Demo.Authorization.Entities;

namespace LeXun.Demo.Authorization.Dtos
{
    /// <summary>
    /// DTO对象映射类
    /// </summary>
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        /// <summary>
        /// 创建对象映射
        /// </summary>
        /// <param name="mapper">映射配置表达</param>
        public void CreateMaps(MapperConfigurationExpression mapper)
        {
            mapper.CreateMap<EntityRoleInputDto, EntityRole>()
                .ForMember(mr => mr.FilterGroupJson, opt => opt.MapFrom(dto => dto.FilterGroup.ToJsonString(false, false)));

            //mapper.CreateMap<EntityRole, EntityRoleOutputDto>()
            //    .ForMember(dto => dto.FilterGroup, opt => opt.ResolveUsing(mr => mr.FilterGroupJson?.FromJsonString<FilterGroup>()));
        }
    }
}