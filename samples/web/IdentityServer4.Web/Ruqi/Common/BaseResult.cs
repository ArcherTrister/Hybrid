namespace FlyingFish.Mobile.Ruqi.Common
{
    /// <summary>
    ///
    /// </summary>
    public sealed class BaseResult
    {
        /// <summary>
        /// 200:处理成功,其他为失败(4001:正在处理中,4002:处理失败)
        /// </summary>
        public int code { get; set; } = 200;

        /// <summary>
        /// 成功为SUCCESS，失败为原因
        /// </summary>
        public string msg { get; set; } = "SUCCESS";
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BaseResult<T>
    {
        /// <summary>
        /// 200:处理成功,其他为失败(4001:正在处理中,4002:处理失败)
        /// </summary>
        public int code { get; set; } = 200;

        /// <summary>
        /// 成功为SUCCESS，失败为原因
        /// </summary>
        public string msg { get; set; } = "SUCCESS";

        /// <summary>
        /// 业务响应数据
        /// </summary>
        public T data { get; set; }
    }
}