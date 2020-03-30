// -----------------------------------------------------------------------
//  <copyright file="AspHybridPack.cs" company="Hybrid��Դ�Ŷ�">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-09 22:20</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Packs;

using Microsoft.AspNetCore.Builder;

namespace Hybrid.AspNetCore
{
    /// <summary>
    ///  ����AspNetCore������Packģ�����
    /// </summary>
    public abstract class AspHybridPack : HybridPack
    {
        /// <summary>
        /// Ӧ��AspNetCore�ķ���ҵ��
        /// </summary>
        /// <param name="app">AspӦ�ó��򹹽���</param>
        public virtual void UsePack(IApplicationBuilder app)
        {
            base.UsePack(app.ApplicationServices);
        }
    }
}