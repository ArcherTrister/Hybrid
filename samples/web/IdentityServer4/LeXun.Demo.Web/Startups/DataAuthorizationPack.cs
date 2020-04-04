// -----------------------------------------------------------------------
//  <copyright file="DataAuthorizationPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-27 0:35</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization;
using Hybrid.Authorization.Dtos;
using Hybrid.Authorization.EntityInfos;
using Hybrid.AutoMapper;

using LeXun.Demo.Authorization;
using LeXun.Demo.Authorization.Dtos;
using LeXun.Demo.Authorization.Entities;

using Microsoft.Extensions.DependencyInjection;

namespace LeXun.Demo.Web.Startups
{
    public class DataAuthorizationPack
        : DataAuthorizationPackBase<DataAuthManager, DataAuthCache, EntityInfo, EntityInfoInputDto, EntityRole, EntityRoleInputDto, int>
    {
        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddSingleton<IAutoMapperConfiguration, AutoMapperConfiguration>();

            return base.AddServices(services);
        }
    }
}