namespace Hybrid.Domain.Entities
{
    /// <summary>
    /// 定义是否启用
    /// </summary>
    public interface IEnabled
    {
        /// <summary>
        /// 获取或设置是否启用
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
