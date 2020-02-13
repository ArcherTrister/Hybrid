using System.Collections.Generic;

namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 试算响应模型
    /// </summary>
    public class RQWithdrawTryCalculateResponse
    {
        /// <summary>
        /// 金额变更粒度，比如提现金额数必须是100整数倍，即传100
        /// </summary>
        public string multiple { get; set; }

        /// <summary>
        /// 用户可提现最小金额，单位元，数值，保留小数点二位
        /// </summary>
        public string minAmount { get; set; }

        /// <summary>
        /// 用户可提现最大金额，单位元，数值，保留小数点二位。
        /// 注意：当 minAmount 与maxAmount相等时，
        /// 平台提现页面里提现金额项默认展示的金额为minAmount，且用户不可更改。
        /// 当两者不相等时，提现页面里提现金额项可供用户可输入
        /// </summary>
        public string maxAmount { get; set; }

        /// <summary>
        /// 实际到账金额，单位元，数值，保留小数点二位。
        /// 合作机构根据用户输入的提现金额（如果给出的 minAmount 不等于maxAmount），计算出用户实际到账金额
        /// </summary>
        public string cardAmount { get; set; }

        /// <summary>
        /// 预计每期还款明细(如果合作机构需要给用户展示明细则传递)。数组元素为object类型，参考RepayPlan详细描述
        /// </summary>
        public List<RQRepayPlan> repayPlan { get; set; } = new List<RQRepayPlan>();

        /// <summary>
        /// 可选期限列表，如可供用户选择的有 1 期/3 期，则传：[“1”,”3”]。数组元素为string类型
        /// </summary>
        public List<string> loanPeriods { get; set; } = new List<string>();

        /// <summary>
        /// 贷款期数
        /// </summary>
        public string loanPeriod { get; set; }

        /// <summary>
        /// 每期天数，按月还款传30 
        /// </summary>
        public string loanPeriodDays { get; set; }

        /// <summary>
        /// 贷款文案描述，与loanPeriod字段一一对应。数组元素为string类型
        /// </summary>
        public List<string> loanDesc { get; set; } = new List<string>();
    }
}