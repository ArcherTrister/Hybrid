// -----------------------------------------------------------------------
//  <copyright file="IModuleFunctionStore.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using Hybrid.Dependency;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hybrid.Zero.Security
{
    /// <summary>
    /// 定义模块功能存储
    /// </summary>
    [IgnoreDependency]
    public interface IModuleFunctionStore<TModuleFunction, in TModuleKey>
    {
        #region 模块功能信息业务

        /// <summary>
        /// 获取 模块功能信息查询数据集
        /// </summary>
        IQueryable<TModuleFunction> ModuleFunctions { get; }

        /// <summary>
        /// 检查模块功能信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的模块功能信息编号</param>
        /// <returns>模块功能信息是否存在</returns>
        Task<bool> CheckModuleFunctionExists(Expression<Func<TModuleFunction, bool>> predicate, Guid id = default(Guid));

        /// <summary>
        /// 设置模块的功能信息
        /// </summary>
        /// <param name="moduleId">模块编号</param>
        /// <param name="functionIds">要设置的功能编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> SetModuleFunctions(TModuleKey moduleId, Guid[] functionIds);

        #endregion 模块功能信息业务
    }
}