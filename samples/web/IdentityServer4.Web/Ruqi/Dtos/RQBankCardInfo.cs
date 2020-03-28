namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 银行/银行卡信息
    /// </summary>
    public class RQBankCardInfo
    {
        /// <summary>
        /// 银行名
        /// </summary>
        public string bankName { get; set; }

        /// <summary>
        /// 银行代码
        /// </summary>
        public string bankCode { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string cardNo { get; set; }

        /// <summary>
        /// 卡类型：1-借记卡，2-信用卡
        /// </summary>
        public int cardType { get; set; }
    }
}