using System.Collections.Generic;
using System.ComponentModel;

namespace NOPI.Tests
{
    public class SalesStatisticsOutput
    {
        /// <summary>
        /// 住宅总套数
        /// </summary>
        public int ResidenceTotalNum { get; set; }

        /// <summary>
        /// 住宅总面积
        /// </summary>
        public decimal ResidenceTotalArea { get; set; }

        /// <summary>
        /// 车库总套数
        /// </summary>
        public int GarageTotalNum { get; set; }

        /// <summary>
        /// 车库总面积
        /// </summary>
        public decimal GarageTotalArea { get; set; }

        /// <summary>
        /// 车位总套数
        /// </summary>
        public int StallTotalNum { get; set; }

        /// <summary>
        /// 车位总面积
        /// </summary>
        public decimal StallTotalArea { get; set; }

        /// <summary>
        /// 商铺总套数
        /// </summary>
        public int BusinessTotalNum { get; set; }

        /// <summary>
        /// 商铺总面积
        /// </summary>
        public decimal BusinessTotalArea { get; set; }

        /// <summary>
        /// 销售统计
        /// </summary>
        public List<SalesOutput> SalesOutputs { get; set; } = new List<SalesOutput>();
    }

    /// <summary>
    /// 销售
    /// </summary>
    public class SalesOutput
    {
        /// <summary>
        /// 房屋状态
        /// </summary>
        public HouseStatusEnum Status { get; set; }

        /// <summary>
        /// 统计集合
        /// </summary>
        public List<StatisticsOutput> StatisticsOutputs { get; set; } = new List<StatisticsOutput>();
    }

    /// <summary>
    /// 统计
    /// </summary>
    public class StatisticsOutput
    {
        /// <summary>
        /// 预付款
        /// </summary>
        public decimal ReserveAmount { get; set; }

        /// <summary>
        /// 首付款
        /// </summary>
        public decimal FirstPayAmount { get; set; }

        /// <summary>
        /// 尾款
        /// </summary>
        public decimal FinalPayAmount { get; set; }

        /// <summary>
        /// 未收款
        /// </summary>
        public decimal Uncollected { get; set; }

        /// <summary>
        /// 合同总价
        /// </summary>
        public decimal ContractTotalPrice { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        public HousePurposeEnum HousePurpose { get; set; }

        /// <summary>
        /// 套数
        /// </summary>
        public int TotalNum { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public decimal TotalArea { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }
    }

    /// <summary>
    /// 房屋用途枚举
    /// </summary>
    public enum HousePurposeEnum
    {
        /// <summary>
        /// 住宅
        /// </summary>
        [Description("住宅")]
        Residence = 10,
        /// <summary>
        /// 车库
        /// </summary>
        [Description("车库")]
        Garage = 1,
        /// <summary>
        /// 车位
        /// </summary>
        [Description("车位")]
        Stall = 2,
        /// <summary>
        /// 商业服务
        /// </summary>
        [Description("商业服务")]
        Business = 31,
        /// <summary>
        /// 办公
        /// </summary>
        [Description("办公")]
        Office = 60,
        /// <summary>
        /// 低层住宅
        /// </summary>
        [Description("低层住宅")]
        Villa = 111,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Others = 80,
        /// <summary>
        /// 物业用房
        /// </summary>
        [Description("物业用房")]
        PropertyRoom = 91,
        /// <summary>
        /// 社区用房
        /// </summary>
        [Description("社区用房")]
        CommunityHousing = 92,
        /// <summary>
        /// 配套用房
        /// </summary>
        [Description("配套用房")]
        AuxiliaryHousing = 93,
        /// <summary>
        /// 公厕
        /// </summary>
        [Description("公厕")]
        PublicToilets = 94,
        /// <summary>
        /// 生鲜超市
        /// </summary>
        [Description("生鲜超市")]
        FreshSupermarket = 95,
        /// <summary>
        /// 幼儿园
        /// </summary>
        [Description("幼儿园")]
        Kindergarten = 96
    }

    /// <summary>
    /// 房屋状态
    /// </summary>
    public enum HouseStatusEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown = -1,
        /// <summary>
        /// 未做预售
        /// </summary>
        [Description("未做预售")]
        NoPreSale = 0,
        /// <summary>
        /// 未预定
        /// </summary>
        [Description("未预定")]
        NoReserve = 1,
        /// <summary>
        /// 已预定
        /// </summary>
        [Description("已预定")]
        Reserved = 2,
        /// <summary>
        /// 已签约
        /// </summary>
        [Description("已签约")]
        Signed = 3,
        /// <summary>
        /// 已备案
        /// </summary>
        [Description("已备案")]
        Recorded = 4,
        /// <summary>
        /// 预告登记
        /// </summary>
        [Description("预告登记")]
        Vormerkung = 5,
        /// <summary>
        /// 查封
        /// </summary>
        [Description("查封")]
        Attachment = 6,
        /// <summary>
        /// 预告抵押登记
        /// </summary>
        [Description("预告抵押登记")]
        StartMortgage = 7,
        /// <summary>
        /// 抵押
        /// </summary>
        [Description("抵押")]
        Mortgage = 8,
        /// <summary>
        /// 限制
        /// </summary>
        [Description("限制")]
        Limit = 9,
        /// <summary>
        /// 筑基
        /// </summary>
        [Description("筑基")]
        Base = 10,
        /// <summary>
        /// 注销
        /// </summary>
        [Description("已注销")]
        Cancel = 11
    }
}
