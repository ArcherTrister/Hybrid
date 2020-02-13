using System.Collections.Generic;

namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 附加协议
    /// </summary>
    public class RQAdditional
    {
        /// <summary>
        /// 附加协议标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 附加协议内容，最多展示3条
        /// </summary>
        public List<string> content { get; set; } = new List<string>();
        /// <summary>
        /// 附加协议合同 ,当请求节点node=3：账单详情页展示(合同) 时，只返一个
        /// </summary>
        public List<RQContract> contractList { get; set; } = new List<RQContract>();
    }
}
