namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 如期用户风控信息查询响应模型
    /// </summary>
    public sealed class RQUserRiskControlInfoResponse
    {
        /// <summary>
        /// 调用方生成的会话唯一标识ID
        /// </summary>
        public string reqUniqueId { get; set; }

        /// <summary>
        /// 平台方生成的订单号
        /// </summary>
        public string orderNo { get; set; }

        /// <summary>
        /// 反撸贷分
        /// </summary>
        public string fldScore { get; set; }

        /// <summary>
        /// 观星分
        /// </summary>
        public string modelScore { get; set; }

        /// <summary>
        /// 玉衡分
        /// </summary>
        public string layerModelScore { get; set; }

        /// <summary>
        /// 风控标签(JSONString)
        /// </summary>
        public string riskLabel { get; set; }

        /// <summary>
        /// 机审建议标签(JSONString)
        /// </summary>
        public string suggestLabel { get; set; }
    }
}