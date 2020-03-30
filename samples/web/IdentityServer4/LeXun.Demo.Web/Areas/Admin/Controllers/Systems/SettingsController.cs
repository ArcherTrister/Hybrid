// -----------------------------------------------------------------------
//  <copyright file="SettingsController.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:50</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Mvc.Filters;
using Hybrid.AspNetCore.UI;
using Hybrid.Authorization.Modules;
using Hybrid.Core.Systems;
using Hybrid.Data;
using Hybrid.Exceptions;

using LeXun.Demo.Systems.Dtos;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LeXun.Demo.Web.Areas.Admin.Controllers
{
    [ModuleInfo(Order = 1, Position = "Systems", PositionName = "系统管理模块")]
    [Description("管理-系统设置")]
    public class SettingsController : AdminApiController
    {
        private readonly IKeyValueStore _keyValueStore;

        /// <summary>
        /// 初始化一个<see cref="SettingsController"/>类型的新实例
        /// </summary>
        public SettingsController(IKeyValueStore keyValueStore)
        {
            _keyValueStore = keyValueStore;
        }

        /// <summary>
        /// 读取设置
        /// </summary>
        /// <param name="root">设置根节点，如投票设置为Votes</param>
        /// <returns>相应节点的设置信息</returns>
        [HttpGet]
        [ModuleInfo]
        [Description("读取设置")]
        public IActionResult Read(string root)
        {
            ISetting setting;
            switch (root)
            {
                case "System":
                    setting = _keyValueStore.GetSetting<SystemSetting>();
                    break;

                default:
                    throw new HybridException($"未知的设置根节点: {root}");
            }

            return Json(new SettingOutputDto(setting));
        }

        /// <summary>
        /// 保存指定设置项
        /// </summary>
        /// <param name="dto">设置信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("保存设置")]
        [UnitOfWork]
        public async Task<AjaxResult> Update(SettingInputDto dto)
        {
            Check.NotNull(dto, nameof(dto));

            Type type = Type.GetType(dto.SettingTypeName);
            if (type == null)
            {
                return new AjaxResult($"设置类型\"{dto.SettingTypeName}\"无法找到");
            }
            ISetting setting = JsonConvert.DeserializeObject(dto.SettingJson, type) as ISetting;
            OperationResult result = await _keyValueStore.SaveSetting(setting);
            if (result.Succeeded)
            {
                return new AjaxResult("设置保存成功");
            }
            return result.ToAjaxResult();
        }
    }
}