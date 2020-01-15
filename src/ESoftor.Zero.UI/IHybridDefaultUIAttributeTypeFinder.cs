using ESoftor.AspNetCore.UI;
using ESoftor.Reflection;
using System.Collections.Generic;
using System.Reflection;

namespace ESoftor.Zero.UI
{
    /// <summary>
    /// 标注了<see cref="HybridDefaultUIAttribute"/>标签的类型查找器
    /// </summary>
    internal interface IHybridDefaultUIAttributeTypeFinder : ITypeFinder
    {
        /// <summary>
        /// 获取TypeInfo列表
        /// </summary>
        IEnumerable<TypeInfo> GetTypeInfos();
    }
}