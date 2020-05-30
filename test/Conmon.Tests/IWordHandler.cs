using Aspose.Words;

using Hybrid.Application.Services.Dtos;

using System.Collections.Generic;
using System.IO;

namespace Conmon.Tests
{
    /// <summary>
    /// 表示对未定义值的变量进行默认值赋值的方法
    /// </summary>
    /// <param name="varname">模板变量名称</param>
    /// <returns>默认值</returns>
    public delegate string DefaultValueHandler(string varname);

    /// <summary>
    /// 表示处理完模板变量及图片渲染后额外操作的方法
    /// </summary>
    /// <param name="doc">word文档对象</param>
    /// <param name="vars">模板变量键值对列表</param>
    public delegate void ExtraHandler(Document doc, List<NameValue> vars);

    /// <summary>
    /// word文档模板处理接口
    /// </summary>
    public interface IWordHandler
    {
        /// <summary>
        /// 解析word模板，并使用用户变量集合生成新的word文档
        /// </summary>
        /// <param name="src">原word模板文件路径</param>
        /// <param name="vars">模板变量集合</param>
        /// <param name="state">任意用户自定义对象</param>
        /// <param name="beforeReplace">模板变量替换具体值之前的方法</param>
        /// <param name="defaultSetter">设置未定义变量默认值的方法</param>
        /// <param name="extraHandler">额外操作</param>
        /// <returns>内存流</returns>
        MemoryStream Process(string src, List<NameValue> vars, object state, DefaultValueHandler defaultSetter, ExtraHandler extraHandler);
    }
}
