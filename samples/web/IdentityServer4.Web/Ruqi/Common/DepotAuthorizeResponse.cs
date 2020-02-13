namespace FlyingFish.Mobile.Ruqi.Common
{
    /// <summary>
    /// 存管授权响应模型
    /// </summary>
    public class DepotAuthorizeResponse
    {
        /// <summary>
        /// 授权H5链接地址(点击该地址可直接打开授权页面)
        /// </summary>
        public string url { get; set; }
    }
}