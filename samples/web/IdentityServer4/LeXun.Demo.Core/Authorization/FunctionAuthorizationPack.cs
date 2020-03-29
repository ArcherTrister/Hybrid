// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthorizationPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-27 0:29</last-date>
// -----------------------------------------------------------------------

using LeXun.Demo.Authorization.Dtos;
using LeXun.Demo.Authorization.Entities;
using LeXun.Demo.Identity;

using Microsoft.Extensions.DependencyInjection;

using Hybrid.AspNetCore.Mvc;
using Hybrid.Authorization;
using Hybrid.Authorization.Dtos;
using Hybrid.Authorization.Functions;
using Hybrid.Core.Packs;
using Hybrid.Entity;


namespace LeXun.Demo.Authorization
{
    [DependsOnPacks(typeof(IdentityPack), typeof(MvcFunctionPack))]
    public class FunctionAuthorizationPack
        : FunctionAuthorizationPackBase<FunctionAuthManager, FunctionAuthorization, FunctionAuthCache, ModuleHandler, Function,
            FunctionInputDto, Module, ModuleInputDto, int, ModuleFunction, ModuleRole, ModuleUser, int, int>
    {
        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddSingleton<ISeedDataInitializer, ModuleSeedDataInitializer>();

            return base.AddServices(services);
        }
    }
}