namespace Hybrid.Zero.IdentityServer4
{
    public class CustomTokenResponse
    {
        public string AccessToken { get; set; }

        public string IdentityToken { get; set; }

        public string TokenType { get; set; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}