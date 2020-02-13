namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 主动还款响应模型
    /// </summary>
    public class RQRepaymentResponse
    {
        /// <summary>
        /// （线下还款）银行卡号
        /// </summary>
        public string repayCardNo { get; set; }

        /// <summary>
        /// （线下还款）银行开户地址
        /// </summary>
        public string repayBankAddress { get; set; }

        /// <summary>
        /// （线下还款）用户名
        /// </summary>
        public string repayUserName { get; set; }

        /// <summary>
        /// （线下还款）银行名称
        /// </summary>
        public string repayBankName { get; set; }
    }
}