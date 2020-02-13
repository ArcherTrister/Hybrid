namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 还款计划，放款后必传
    /// </summary>
    public class RQRepaymentPlan
    {
        /// <summary>
        /// 实际还款日期 ，格式：yyyy-MM-dd注：在实际还款完成必传
        /// </summary>
        public string trueRepaymentTime { get; set; }

        /// <summary>
        /// 计划还款日期，格式：yyyy-MM-dd
        /// </summary>
        public string planRepaymentTime { get; set; }

        /// <summary>
        /// 本期账单金额中的本金，单位元，数值，保留小数点二位
        /// </summary>
        public string principal { get; set; }

        /// <summary>
        /// 费用，，单位元，数值，保留小数点二位
        /// </summary>
        public string free { get; set; }

        /// <summary>
        /// 本期利息费，单位元，数值，保留小数点二位
        /// </summary>
        public string interest { get; set; }

        /// <summary>
        /// 已还金额，，单位元，数值，保留小数点二位。还清后传已还总金额
        /// </summary>
        public string repaidAmount { get; set; }

        /// <summary>
        /// 期数
        /// </summary>
        public int period { get; set; }

        /// <summary>
        /// 还款计划状态：1-待还款，2-已逾期，3-已结清，4-逾期结清
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 逾期罚款，单位元，数值，保留小数点二位
        /// </summary>
        public string overdueFee { get; set; }

        /// <summary>
        /// 逾期天数(如：10)
        /// </summary>
        public int overdueDay { get; set; }

        /// <summary>
        /// 是否逾期，0-未逾期，1-逾期（注意：逾期未还，status要为2；逾期结清,status要为4）
        /// </summary>
        public int overdue { get; set; }

        /// <summary>
        /// 本期还款总金额，本金+利息+费用+逾期费，单位元，数值，保留小数点二位
        /// </summary>
        public string amount { get; set; }
    }
}
