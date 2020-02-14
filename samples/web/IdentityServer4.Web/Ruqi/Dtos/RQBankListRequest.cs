namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 银行列表请求模型
    /// </summary>
    public class RQBankListRequest
    {
        /// <summary>
        /// 银行卡类型：1-借记卡，2-信用卡
        /// </summary>
        public string cardType { get; set; }
    }
}
