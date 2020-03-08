﻿using System;
using System.ComponentModel;

using Microsoft.Extensions.DependencyInjection;

using Hybrid.Authorization.Dtos;
using Hybrid.Authorization.Entities;
using Hybrid.Authorization.EntityInfos;
using Hybrid.Core.Modules;
using Hybrid.EventBuses;

namespace Hybrid.Authorization
{
    /// <summary>
    /// 数据权限模块基类
    /// </summary>
    /// <typeparam name="TDataAuthorizationManager">数据权限管理器</typeparam>
    /// <typeparam name="TDataAuthCache">数据权限缓存</typeparam>
    /// <typeparam name="TEntityInfo">数据实体类型</typeparam>
    /// <typeparam name="TEntityInfoInputDto">数据实体输入DTO类型</typeparam>
    /// <typeparam name="TEntityRole">实体角色类型</typeparam>
    /// <typeparam name="TEntityRoleInputDto">实体角色输入DTO类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    [Description("数据权限模块")]
    [DependsOnModules(typeof(EventBusModule))]
    public abstract class DataAuthorizationModuleBase<TDataAuthorizationManager, TDataAuthCache, TEntityInfo, TEntityInfoInputDto, TEntityRole,
        TEntityRoleInputDto, TRoleKey> : HybridModule
        where TDataAuthorizationManager : class,
            IEntityInfoStore<TEntityInfo, TEntityInfoInputDto>,
            IEntityRoleStore<TEntityRole, TEntityRoleInputDto, TRoleKey>
        where TDataAuthCache : IDataAuthCache
        where TEntityInfo : IEntityInfo
        where TEntityInfoInputDto : EntityInfoInputDtoBase
        where TEntityRole : EntityRoleBase<TRoleKey>
        where TEntityRoleInputDto : EntityRoleInputDtoBase<TRoleKey>
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Application;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 3;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IDataAuthCache), typeof(TDataAuthCache));

            services.AddScoped<TDataAuthorizationManager>();
            services.AddScoped(typeof(IEntityInfoStore<TEntityInfo, TEntityInfoInputDto>), provider => provider.GetService<TDataAuthorizationManager>());
            services.AddScoped(typeof(IEntityRoleStore<TEntityRole, TEntityRoleInputDto, TRoleKey>), provider => provider.GetService<TDataAuthorizationManager>());

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UseModule(IServiceProvider provider)
        {
            IEntityInfoHandler entityInfoHandler = provider.GetService<IEntityInfoHandler>();
            entityInfoHandler.RefreshCache();

            IFunctionAuthCache functionAuthCache = provider.GetService<IFunctionAuthCache>();
            functionAuthCache.BuildRoleCaches();

            IDataAuthCache dataAuthCache = provider.GetService<IDataAuthCache>();
            dataAuthCache.BuildCaches();

            IsEnabled = true;
        }
    }
}