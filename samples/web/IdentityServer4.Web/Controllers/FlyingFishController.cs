using FlyingFish.Mobile.Ruqi.Common;
using FlyingFish.Mobile.Ruqi.Dtos;
using Hybrid.AspNetCore.Mvc.Controllers;
using Hybrid.AspNetCore.UI;
using Hybrid.Core.ModuleInfos;

using IdentityServer4.Web.Services;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace IdentityServer4.Web.Controllers
{
    /// <summary>
    /// 飞鱼服务
    /// </summary>
    [Description("网站-飞鱼服务")]
    [ModuleInfo(Order = 2)]
    public sealed class FlyingFishController : LocalApiController
    {
        private readonly IFlyingFishService _flyingFishService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flyingFishService"></param>
        public FlyingFishController(IFlyingFishService flyingFishService)
        {
            _flyingFishService = flyingFishService;
        }

        /// <summary>
        /// 用户检测
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public async Task<AjaxResult<object>> CheckUser(RQBaseRequest request)
        {
            return await _flyingFishService.CheckUserAsync(request);
        }

        /// <summary>
        /// 推送基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult> PushPhaseOne(RQBaseRequest request)
        {
            return await _flyingFishService.PushPhaseOneAsync(request);
        }

        /// <summary>
        /// 推送补充信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult> PushPhaseTwo(RQBaseRequest request)
        {
            return await _flyingFishService.PushPhaseTwoAsync(request);
        }

        /// <summary>
        /// 银行列表
        /// </summary>
        /// <param name="request">银行列表请求模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<RQBankInfo>>> BankList(RQBankListRequest request)
        {
            return await _flyingFishService.BankListAsync(request);
        }

        /// <summary>
        /// 银行卡信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<RQCardListResponse>> CardList(RQBaseRequest request)
        {
            return await _flyingFishService.CardListAsync(request);
        }

        /// <summary>
        /// 绑卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<RQBindCardResponse>> BindCard(RQBaseRequest request)
        {
            return await _flyingFishService.BindCardAsync(request);
        }

        /// <summary>
        /// 跳转到机构绑卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<BindCardExtResponse>> BindCardExt(RQBaseRequest request)
        {
            return await _flyingFishService.BindCardExtAsync(request);
        }

        /// <summary>
        /// 试算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<RQWithdrawTryCalculateResponse>> WithdrawTryCalculate(RQBaseRequest request)
        {
            return await _flyingFishService.WithdrawTryCalculateAsync(request);
        }

        /// <summary>
        /// 确认借款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult> ConfirmLoan(RQBaseRequest request)
        {
            return await _flyingFishService.ConfirmLoanAsync(request);
        }

        /// <summary>
        /// 合同
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<RQLoanContractExtResponse>> LoanContractExt(RQBaseRequest request)
        {
            return await _flyingFishService.LoanContractExtAsync(request);
        }

        /// <summary>
        /// 订单状态查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<RQPullOrderStatusResponse>> PullOrderStatus(RQBaseRequest request)
        {
            return await _flyingFishService.PullOrderStatusAsync(request);
        }

        /// <summary>
        /// 存管授权
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<DepotAuthorizeResponse>> DepotAuthorize(RQBaseRequest request)
        {
            return await _flyingFishService.DepotAuthorizeAsync(request);
        }

        /// <summary>
        /// 存管提现
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<DepotWithdrawResponse>> DepotWithdraw(RQBaseRequest request)
        {
            return await _flyingFishService.DepotWithdrawAsync(request);
        }

        /// <summary>
        /// 主动还款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<RQRepaymentResponse>> Repayment(RQBaseRequest request)
        {
            return await _flyingFishService.RepaymentAsync(request);
        }

        /// <summary>
        /// 跳转到机构还款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<RepaymentExtResponse>> RepaymentExt(RQBaseRequest request)
        {
            return await _flyingFishService.RepaymentExtAsync(request);
        }

        /// <summary>
        /// 跳转机构认证（暂不开放）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<AuthorizeExtResponse>> AuthorizeExt(RQBaseRequest request)
        {
            ////_iRSAHelper.RSAHelpe.VerifyAsync(request.biz_data, request.sign);
            ////AuthorizeExtRequest authorizeExtRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorizeExtRequest>Async(request.biz_data);
            //await Task.CompletedTask;
            //return new AjaxResult<AuthorizeExtResponse> { Success=false, Content = "暂不开放", Result = null };
            ////return new AjaxResult<AuthorizeExtResponse> { code = 200, data = await _flyingFishService.AuthorizeExtAsync(request) };
            return await Task.FromResult(new AjaxResult<AuthorizeExtResponse> { Success = false, Content = "暂不开放", Result = null });
        }
    }
}
