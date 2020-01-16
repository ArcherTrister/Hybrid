// -----------------------------------------------------------------------
//  <copyright file="IModuleStore.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using Hybrid.Dependency;
using Hybrid.Domain.Entities;
using Hybrid.Zero.Security.Dtos;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hybrid.Zero.Security
{
    /// <summary>
    /// 定义模块信息的存储
    /// </summary>
    [IgnoreDependency]
    public interface IModuleStore<TModule, in TModuleInputDto, TModuleKey>
        where TModule : ModuleBase<TModuleKey>, IEntity<TModuleKey>
        where TModuleInputDto : ModuleInputDtoBase<TModuleKey>
        where TModuleKey : struct, IEquatable<TModuleKey>
    {
        #region 模块信息业务

        /// <summary>
        /// 获取 模块信息查询数据集
        /// </summary>
        IQueryable<TModule> Modules { get; }

        /// <summary>
        /// 检查模块信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的模块信息编号</param>
        /// <returns>模块信息是否存在</returns>
        Task<bool> CheckModuleExists(Expression<Func<TModule, bool>> predicate, TModuleKey id = default(TModuleKey));

        /// <summary>
        /// 添加模块信息
        /// </summary>
        /// <param name="dto">要添加的模块信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateModule(TModuleInputDto dto);

        /// <summary>
        /// 更新模块信息
        /// </summary>
        /// <param name="dto">包含更新信息的模块信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateModule(TModuleInputDto dto);

        /// <summary>
        /// 删除模块信息
        /// </summary>
        /// <param name="id">要删除的模块信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteModule(TModuleKey id);

        /// <summary>
        /// 获取树节点及其子节点的所有模块编号
        /// </summary>
        /// <param name="rootIds">树节点</param>
        /// <returns>模块编号集合</returns>
        TModuleKey[] GetModuleTreeIds(params TModuleKey[] rootIds);

        #endregion 模块信息业务
    }
}