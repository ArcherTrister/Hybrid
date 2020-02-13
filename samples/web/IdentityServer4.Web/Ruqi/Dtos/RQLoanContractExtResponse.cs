
using System.Collections.Generic;

namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 合同响应模型
    /// </summary>
    public class RQLoanContractExtResponse
    {
        /// <summary>
        /// 合同协议
        /// </summary>
        public List<RQContract> contract { get; set; } = new List<RQContract>();
        /// <summary>
        /// 附加协议
        /// </summary>
        public RQAdditional additional { get; set; } = new RQAdditional();
    }
}