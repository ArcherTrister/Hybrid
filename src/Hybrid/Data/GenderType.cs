using System.ComponentModel;

namespace Hybrid.Data
{
    /// <summary>
    /// 性别类型
    /// </summary>
    public enum GenderType
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female,

        /// <summary>
        /// 保密
        /// </summary>
        [Description("保密")]
        Security,

        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Uncharted
    }
}
