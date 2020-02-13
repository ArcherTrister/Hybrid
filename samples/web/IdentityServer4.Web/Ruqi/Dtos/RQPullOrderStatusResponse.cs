using System.Collections.Generic;

namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 订单状态查询响应模型
    /// </summary>
    public class RQPullOrderStatusResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderNo { get; set; }

        /// <summary>
        /// 订单状态： 100-待补充资料，200-待审核，201-审核失败，210-待绑卡，211-待签约，
        /// 220-待确认，300-放款中，301-待提现，310-待还款，320-已还清，321-逾期还清，322-已取消
        /// </summary>
        public int orderStatus { get; set; }

        /// <summary>
        /// 审批金额，单位元，数值，保留小数点二位。审批通过后必传
        /// </summary>
        public string approvalAmount { get; set; }

        /// <summary>
        /// 审批期数，审批通过后必传
        /// </summary>
        public int approvalPeriods { get; set; }

        /// <summary>
        /// 审批每期天数，审批通过后必传 （月按30计算）
        /// </summary>
        public int approvalPeriodDays { get; set; }

        /// <summary>
        /// 确认借款/合同金额，单位元，数值，保留小数点二位。确认借款后必传
        /// </summary>
        public string loanAmount { get; set; }

        /// <summary>
        /// 确认借款/合同期数，确认借款后必传
        /// </summary>
        public int loanPeriods { get; set; }

        /// <summary>
        /// 确认借款/合同每期天数，确认借款后必传（月按30计算）
        /// </summary>
        public int loanPeriodDays { get; set; }

        /// <summary>
        /// 实际到手金额，单位元，数值，保留小数点二位。确认借款后必传
        /// </summary>
        public string cardAmount { get; set; }

        /// <summary>
        /// 是否可换卡，1-是，0-否
        /// </summary>
        public int changeCardFlag { get; set; }

        /// <summary>
        /// 0-不需要存管账户提款，1-需要去存管账户提款，不需要再次去提时需再传0过来。放款后必传
        /// </summary>
        public int withdrawFlag { get; set; }

        /// <summary>
        /// 1-支持还款账户于还款日进行自动划扣（如果机构支持主动还款，用户也可进行主动还款）。 
        /// 2-不支持还款账户于还款日进行自动划扣(机构必须支持用户主动还款)。 放款后必传
        /// </summary>
        public int autopayFlag { get; set; }

        /// <summary>
        /// 用户放款/还款银行卡，绑卡后必传。数组元素是object类型，参考BankCardInfo详细说明。注意类型为数组。
        /// 如果机构告知平台方无需绑卡，即用户在机构方已有绑卡信息，也必须传
        /// </summary>
        public List<RQBankCardInfo> bankCardInfo { get; set; } = new List<RQBankCardInfo>();

        /// <summary>
        /// 还款计划，放款后必传。数组元素是object类型，参考RepaymentPlan详细说明。注意类型为数组
        /// </summary>
        public List<RQRepaymentPlan> repaymentPlan { get; set; } = new List<RQRepaymentPlan>();
    }
}