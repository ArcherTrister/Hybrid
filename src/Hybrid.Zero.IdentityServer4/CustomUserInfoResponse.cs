using Newtonsoft.Json;

using System;

namespace Hybrid.Zero.IdentityServer4
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    public class CustomUserInfoResponse
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        //[JsonProperty("sub")]
        public string Sub { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        //[JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        //[JsonProperty("TrueName")]
        public string TrueName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        //[JsonProperty("IdCard")]
        public string IdCard { get; set; }

        /// <summary>
        /// 身份证验证状态
        /// </summary>
        //[JsonProperty("IdCardVerified")]
        public string IdCardVerified { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        //[JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty("nick_name")]
        public string NickName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        //[JsonProperty("AvatarUrl")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        //[JsonProperty("email")]
        public string Email { get; set; }

        //[JsonProperty("sub")]
        //public string TenantId { get; set; }
    }
}