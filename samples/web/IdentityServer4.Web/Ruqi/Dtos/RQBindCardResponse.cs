namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 绑卡响应模型
    /// </summary>
    public class RQBindCardResponse
    {
        /// <summary>
        /// 绑卡是否需要发送验证码：1-是，0-否；当
        /// 需要验证码时，机构需要同步给用户发送验
        /// 证码，飞鱼会携带验证码再次请求该接口
        /// </summary>
        public int bindStatus { get; set; }
    }
}