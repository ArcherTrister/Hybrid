namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 当前选择的期限下每一期应还金额和应还款日期
    /// </summary>
    public class RQLoanPeriod
    {
        /// <summary>
        /// 期数
        /// </summary>
        public string period { get; set; }

        /// <summary>
        /// 应还款日期（yyyy-MM-dd）
        /// </summary>
        public string repayTime { get; set; }

        /// <summary>
        /// 应还款金额,单位元，数值，保留小数点二位
        /// </summary>
        public string repayAmount { get; set; }
    }
}
