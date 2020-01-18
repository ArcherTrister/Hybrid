using System.Collections.Generic;

namespace Test.Web
{
    public class SqlServer
    {
        /// <summary>
        /// 
        /// </summary>
        public string DbContextTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DatabaseType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LazyLoadingProxiesEnabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AuditEntityEnabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AutoMigrationEnabled { get; set; }
    }

    public class DbContexts
    {
        /// <summary>
        /// 
        /// </summary>
        public SqlServer SqlServer { get; set; }
    }

    /// <summary>
    /// 第三方OAuth2登录的配置选项
    /// </summary>
    public class OAuth2Options
    {
        /// <summary>
        /// 获取或设置 本应用在第三方OAuth2系统中的客户端Id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 获取或设置 本应用在第三方OAuth2系统中的客户端密钥
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 获取或设置 是否启用
        /// </summary>
        public bool Enabled { get; set; }
    }

    public class HealthChecks
    {
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PrivateMemory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int VirtualMemorySize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int WorkingSet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Enabled { get; set; }
    }

    public class MailSender
    {
        /// <summary>
        /// 
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 柳柳发件测试
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
    }

    public class Jwt
    {
        /// <summary>
        /// 
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AccessExpireMins { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RefreshExpireMins { get; set; }
    }

    public class Redis
    {
        /// <summary>
        /// 
        /// </summary>
        public string Configuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string InstanceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Enabled { get; set; }
    }

    public class Swagger
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MiniProfiler { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Enabled { get; set; }
    }

    public class Hangfire
    {
        /// <summary>
        /// 
        /// </summary>
        public int WorkerCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StorageConnectionString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DashboardUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Roles { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Enabled { get; set; }
    }

    public class Exceptionless
    {
        /// <summary>
        /// 
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServerUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Enabled { get; set; }
    }

    public class HybridOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public DbContexts DbContexts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, OAuth2Options> OAuth2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HealthChecks HealthChecks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MailSender MailSender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Jwt Jwt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Redis Redis { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Swagger Swagger { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Hangfire Hangfire { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Exceptionless Exceptionless { get; set; }
    }
}
