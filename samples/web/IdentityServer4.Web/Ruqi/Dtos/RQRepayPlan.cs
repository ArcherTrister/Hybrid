using System.Collections.Generic;

namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 预计每期还款明细(如果合作机构需要给用户展示明细则传递)
    /// </summary>
    public class RQRepayPlan
    {
        /// <summary>
        /// 当前选择的期限应还总金额,单位元，数值，保留小数点二位
        /// </summary>
        public string sumAmount { get; set; }

        /// <summary>
        /// 当前选择的期限下每一期应还金额和应还款日期。数组元素为object类型，参考LoanPeriod详细描述
        /// </summary>
        public List<RQLoanPeriod> loanPeriod { get; set; } = new List<RQLoanPeriod>();

        /// <summary>
        /// 对应期限
        /// </summary>
        public int loanPeriodValue { get; set; }
    }
}
