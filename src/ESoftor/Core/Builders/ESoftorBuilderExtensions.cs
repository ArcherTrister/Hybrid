// -----------------------------------------------------------------------
//  <copyright file="ESoftorBuilderExtensions.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-02 14:27</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Modules;

namespace ESoftor.Core.Builders
{
    /// <summary>
    /// IESoftorBuilder扩展方法
    /// </summary>
    public static class ESoftorBuilderExtensions
    {
        /// <summary>
        /// 添加CoreModule
        /// </summary>
        public static IESoftorBuilder AddCoreModule(this IESoftorBuilder builder)
        {
            return builder.AddModule<ESoftorCoreModule>();
        }
    }
}