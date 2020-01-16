// -----------------------------------------------------------------------
//  <copyright file="IHybridModuleManager.cs" company="cn.lxking">
//      Copyright ? 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-10 0:12</last-date>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;

namespace Hybrid.Core.Modules
{
    /// <summary>
    /// ����Hybridģ�������
    /// </summary>
    public interface IHybridModuleManager
    {
        /// <summary>
        /// ��ȡ �Զ�������������ģ����Ϣ
        /// </summary>
        IEnumerable<HybridModule> SourceModules { get; }

        /// <summary>
        /// ��ȡ ���ռ��ص�ģ����Ϣ����
        /// </summary>
        IEnumerable<HybridModule> LoadedModules { get; }

        /// <summary>
        /// ����ģ�����
        /// </summary>
        /// <param name="services">��������</param>
        /// <returns>��������</returns>
        IServiceCollection LoadModules(IServiceCollection services);

        /// <summary>
        /// Ӧ��ģ�����
        /// </summary>
        /// <param name="provider">�����ṩ��</param>
        void UseModule(IServiceProvider provider);
    }
}