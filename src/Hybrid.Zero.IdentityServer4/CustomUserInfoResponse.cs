namespace Hybrid.Zero.IdentityServer4
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class CustomUserInfoResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string TrueName { get; set; }
        public string IdCard { get; set; }
        public string IdCardVerified { get; set; }
        public string Gender { get; set; }
        public string NickName { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarUrl { get; set; }
        public string Email { get; set; }
        //public string TenantId { get; set; }
    }
}