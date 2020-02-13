using System;

namespace FlyingFish.Mobile.Ruqi.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class RQCallBackBaseRequest
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public string signType { get; set; } = "RSA";

        /// <summary>
        /// 请求的业务数据
        /// </summary>
        public string bizData { get; set; }

        /// <summary>
        /// 机构id
        /// </summary>
        public string appId { get; set; } = "3000002";

        /// <summary>
        /// 接口版本
        /// </summary>
        public string version { get; set; } = "1.0.0";

        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");

        /// <summary>
        /// 要请求的API方法名
        /// </summary>
        public string call { get; set; }
    }
}
