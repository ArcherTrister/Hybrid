// -----------------------------------------------------------------------
//  <copyright file="CommonController.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.AspNetCore.Mvc.Controllers;
using ESoftor.AspNetCore.Services;
using ESoftor.AspNetCore.UI;
using ESoftor.CodeGenerator;
using ESoftor.Core.ModuleInfos;
using ESoftor.Core.Modules;
using ESoftor.Data;
using ESoftor.Drawing;
using ESoftor.IO;
using ESoftor.Reflection;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using AssemblyExtensions = ESoftor.Reflection.AssemblyExtensions;

namespace IdentityServer4.Web.Controllers
{
    [Description("网站-通用")]
    [ModuleInfo(Order = 3)]
    public class CommonController : LocalApiController
    {
        private readonly IVerifyCodeService _verifyCodeService;
        private readonly IWebHostEnvironment _environment;

        public CommonController(
            IWebHostEnvironment environment,
            IVerifyCodeService verifyCodeService
            )
        {
            _verifyCodeService = verifyCodeService;
            _environment = environment;
        }

        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <returns>验证码图片文件</returns>
        [HttpGet]
        [ModuleInfo]
        [AllowAnonymous]
        [Description("验证码")]
        public string VerifyCode()
        {
            ValidateCoder coder = new ValidateCoder()
            {
                RandomColor = true,
                RandomItalic = true,
                RandomLineCount = 7,
                RandomPointPercent = 10,
                RandomPosition = true
            };
            Bitmap bitmap = coder.CreateImage(4, out string code);
            _verifyCodeService.SetCode(code, out string id);
            return _verifyCodeService.GetImageString(bitmap, id);
        }

        /// <summary>
        /// 验证验证码的有效性，只作为前端Ajax验证，验证成功不移除验证码，验证码仍需传到后端进行再次验证
        /// </summary>
        /// <param name="code">验证码字符串</param>
        /// <param name="id">验证码编号</param>
        /// <returns>是否无效</returns>
        [HttpGet]
        [ModuleInfo]
        [AllowAnonymous]
        [Description("验证验证码的有效性")]
        public bool CheckVerifyCode(string code, string id)
        {
            return _verifyCodeService.CheckCode(code, id, false);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        [HttpPost]
        [ModuleInfo]
        [AllowAnonymous]
        [Description("上传图片")]
        public async Task<AjaxResult> UploadImage(IFormFile file)
        {
            string fileName = file.FileName;
            fileName = $"{Path.GetFileNameWithoutExtension(fileName)}-{DateTime.Now:MMddHHmmssff}{Path.GetExtension(fileName)}";
            string dir = Path.Combine(_environment.WebRootPath, "upload-files");
            DirectoryHelper.CreateIfNotExists(dir);
            string filePath = dir + $"\\{fileName}";
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return new AjaxResult("上传成功", AjaxResultType.Success, $"upload-files/{fileName}");
        }

        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns>系统信息</returns>
        [HttpGet]
        [ModuleInfo]
        [AllowAnonymous]
        [Description("系统信息")]
        public object SystemInfo()
        {
            IServiceProvider provider = HttpContext.RequestServices;

            dynamic info = new ExpandoObject();
            IESoftorModuleManager moduleManager = provider.GetService<IESoftorModuleManager>();
            info.Modules = moduleManager.SourceModules.OrderBy(m => m.Level).ThenBy(m => m.Order).ThenBy(m => m.GetType().FullName).Select(m => new
            {
                m.GetType().Name,
                Class = m.GetType().FullName,
                Level = m.Level.ToString(),
                m.Order,
                m.IsEnabled
            }).ToList();

            string cliVersion = AssemblyExtensions.GetCliVersion();
            string eSoftorVersion = Assembly.GetExecutingAssembly().GetProductVersion();

            info.Object = new
            {
                Message = "WebApi 数据服务已启动",
                CliVersion = cliVersion,
                ESoftorVersion = eSoftorVersion
            };

            return info;
        }

        /// <summary>
        /// 获取分类类型元数据
        /// </summary>
        /// <param name="type">类型分类，entity,inputdto,outputdto</param>
        /// <param name="handler">类型元数据处理器</param>
        /// <returns></returns>
        [HttpGet]
        [ModuleInfo]
        [AllowAnonymous]
        [Description("获取分类类型元数据")]
        public TypeMetadata[] GetTypeMetadatas(string type, [FromServices]ITypeMetadataHandler handler)
        {
            if (handler == null)
            {
                return new TypeMetadata[0];
            }
            switch (type?.ToLower())
            {
                case "entity":
                    return handler.GetEntityTypeMetadatas();

                case "inputdto":
                    return handler.GetInputDtoMetadatas();

                case "outputdto":
                    return handler.GetOutputDtoMetadata();
            }
            return new TypeMetadata[0];
        }

        /// <summary>
        /// 获取指定类型的元数据
        /// </summary>
        /// <param name="typeFullName">类型命名</param>
        /// <param name="handler">处理器</param>
        /// <returns>类型元数据</returns>
        [HttpGet]
        [ModuleInfo]
        [AllowAnonymous]
        [Description("获取类型元数据")]
        public TypeMetadata GeTypeMetadata(string typeFullName, [FromServices] ITypeMetadataHandler handler)
        {
            if (handler == null)
            {
                return null;
            }
            Type type = Type.GetType(typeFullName);
            if (type == null)
            {
                return null;
            }
            return handler.GetTypeMetadata(type);
        }
    }
}