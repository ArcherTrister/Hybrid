using System.ComponentModel;

namespace Hybrid.Quartz
{
    /// <summary>
    /// Quartz模块
    /// </summary>
    [Description("Quartz模块")]
    public class QuartzModule : QuartzModuleBase
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动
        /// </summary>
        public override int Order => 4;
    }
}