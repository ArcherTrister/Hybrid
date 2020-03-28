namespace FlyingFish.Mobile.Ruqi.Common
{
    /// <summary>
    /// 如期常量
    /// </summary>
    public sealed class RuqiConsts
    {
        /// <summary>
        /// 请求如期接口客户端名称
        /// </summary>
        public const string RuqiHttpClient = "RuqiHttpClient";

        /// <summary>
        /// 如期api地址
        /// </summary>
        public sealed class Urls
        {
            /// <summary>
            /// 用户检测接口（如期科技提供）
            /// </summary>
            public const string CheckUser = "/ruqi-ruxin-loadflow/ruxin/access.htm";

            /// <summary>
            /// 推送基本信息接口（如期科技提供）
            /// </summary>
            public const string PushPhaseOne = "/ruqi-ruxin-loadflow/ruxin/pushBaseInfo.htm";

            /// <summary>
            /// 推送补充信息接口（如期科技提供）
            /// </summary>
            public const string PushPhaseTwo = "/ruqi-ruxin-loadflow/ruxin/updateSupplyInfo.htm";

            /// <summary>
            /// 银行列表接口（如期科技提供）
            /// </summary>
            public const string BankList = "/ruqi-ruxin-loadflow/ruxin/getSupportedBanks.htm";

            /// <summary>
            /// 银行卡信息接口（如期科技提供）
            /// </summary>
            public const string CardList = "/ruqi-ruxin-loadflow/ruxin/getUserBankList.htm";

            /// <summary>
            /// 绑卡接口（如期科技提供）
            /// </summary>
            public const string BindCard = "/ruqi-ruxin-loadflow/ruxin/sendCodeForBindCard.htm";

            /// <summary>
            /// 绑定银行卡扩展
            /// </summary>
            public const string BindCardExt = "";

            /// <summary>
            /// 存管提现
            /// </summary>
            public const string DepotWithdraw = "";

            /// <summary>
            /// 存管授权
            /// </summary>
            public const string DepotAuthorize = "";

            /// <summary>
            /// 跳转到机构还款
            /// </summary>
            public const string RepaymentExt = "";

            /// <summary>
            /// 试算接口（合作方提供）
            /// </summary>
            public const string WithdrawTryCalculate = "/ruqi-ruxin-loadflow/ruxin/orderTrial.htm";

            /// <summary>
            /// 确认借款接口（如期科技提供）
            /// </summary>
            public const string ConfirmLoan = "/ruqi-ruxin-loadflow/ruxin/approvalConfirm.htm";

            /// <summary>
            /// 合同接口（如期科技提供）
            /// </summary>
            public const string LoanContractExt = "/ruqi-ruxin-loadflow/ruxin/getContractList.htm";

            /// <summary>
            /// 订单状态查询接口（如期科技提供）
            /// </summary>
            public const string PullOrderStatus = "/ruqi-ruxin-loadflow/ruxin/getOrderStatus.htm";

            /// <summary>
            /// 主动还款接口（合作机构提供）
            /// </summary>
            public const string Repayment = "/ruqi-ruxin-loadflow/ruxin/repayExecute.htm";

            //拉取签约结果接口    test3-ruxin.ruqiapp.com/ruqi-ruxin-loadflow/ruxin/pullApprovalResults.htm
        }
    }
}