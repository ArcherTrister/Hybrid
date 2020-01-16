using System.ComponentModel;

namespace Hybrid.Quartz.Dashboard
{
    /// <summary>
    /// 验证类型
    /// </summary>
    public enum AuthTypes
    {
        [Description("全部验证")]
        All,

        [Description("验证操作")]
        Operate
    }
}