using System.Collections.Generic;

namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 银行卡信息响应模型
    /// </summary>
    public class RQCardListResponse
    {
        /// <summary>
        /// 是否可添加新卡：1-是，0-否
        /// </summary>
        public int addCardFlag { get; set; }

        /// <summary>
        /// 自定义文案描述(已绑列表页展示给用户提示，可不传)
        /// </summary>
        public string customDesc { get; set; }

        /// <summary>
        /// 银行卡列表。数组元素是object类型
        /// </summary>
        public List<RQBankCardInfo> cardList { get; set; } = new List<RQBankCardInfo>();
    }
}