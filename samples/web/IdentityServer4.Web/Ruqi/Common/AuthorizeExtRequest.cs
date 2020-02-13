namespace FlyingFish.Mobile.Ruqi.Common
{
    /// <summary>
    /// 跳转机构认证请求模型
    /// </summary>
    public class AuthorizeExtRequest
    {
        /// <summary>
        /// 飞鱼订单号
        /// </summary>
        public string orderNo { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 用户身份证
        /// </summary>
        public string idNumber { get; set; }

        /// <summary>
        /// 认证操作成功回调地址 (授权完成需回调该地址)。如果回调页面作为参数附加在跳转落地页的地址上，建议先URLEncode，回跳时URLDecode
        /// </summary>
        public string successReturnUrl { get; set; }

        /// <summary>
        /// 认证项编码，转认证结果反馈接口需传回
        /// </summary>
        public string authCode { get; set; }
    }
}