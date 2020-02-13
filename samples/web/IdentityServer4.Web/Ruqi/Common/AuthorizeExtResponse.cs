namespace FlyingFish.Mobile.Ruqi.Common
{
    /// <summary>
    /// 跳转机构认证响应模型
    /// </summary>
    public class AuthorizeExtResponse
    {
        /// <summary>
        /// 认证到期日期yyyy-MM-dd，如果code=200 该字段必传
        /// </summary>
        public string auth_expires_date { get; set; }

        /// <summary>
        /// 认证链接h5地址（点击该链接地址可直接打开认证页面），如果code=401 该字段必传
        /// </summary>
        public string url { get; set; }
    }
}