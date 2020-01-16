// -----------------------------------------------------------------------
//  <copyright file="IEntityRegisterManager.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-03-08 2:39</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.EntityFrameworkCore
{
    /// <summary>
    /// 定义实体管理器
    /// </summary>
    public interface IEntityManager
    {
        /// <summary>
        /// 初始化实体类型注册
        /// </summary>
        void Initialize();

        /// <summary>
        /// 获取指定上下文类型的实体配置注册信息
        /// </summary>
        /// <param name="dbContextType">数据上下文类型</param>
        /// <returns></returns>
        IEntityRegister[] GetEntityRegisters(Type dbContextType);

        /// <summary>
        /// 获取 实体类所属的数据上下文类
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns>数据上下文类型</returns>
        Type GetDbContextTypeForEntity(Type entityType);
    }
}