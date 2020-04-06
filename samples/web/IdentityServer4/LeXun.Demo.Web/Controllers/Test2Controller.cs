//using Hybrid.Authorization.Modules;
//using Hybrid.Collections;
//using Hybrid.Core.Options;
//using Hybrid.Entity;
//using Hybrid.Redis;

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyModel;

//using System;
//using System.ComponentModel;
//using System.Linq;

////using StackExchange.Profiling.Internal;

//namespace LeXun.Demo.Web.Controllers
//{
//    public class Test2Controller : SiteApiController
//    {
//        private readonly DefaultDbContext _dbContext;

//        public Test2Controller(DefaultDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        [HttpGet]
//        [Description("测试一下")]
//        public IActionResult Test01()
//        {
//            RedisClient redis = new RedisClient();
//            redis.StringSet("Test:Key001", "value001", TimeSpan.FromSeconds(20));
//            return Content(redis.StringGet("Test:Key001"));
//        }

//        /// <summary>
//        /// 功能注释
//        /// </summary>
//        /// <returns>返回数据</returns>
//        [HttpGet]
//        [ModuleInfo]
//        [Description("测试一下")]
//        public string Test02()
//        {
//            var val = AppSettingsReader.GetValue<string>("Hybrid:DbContexts:SqlServer:DbContextTypeName");
//            //return val.ToJson();

//            return DependencyContext.Default.CompileLibraries.Select(m => $"{m.Name},{m.Version}").ExpandAndToString("\r\n");
//        }
//    }
//}