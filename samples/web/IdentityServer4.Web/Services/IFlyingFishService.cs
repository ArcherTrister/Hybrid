using FlyingFish.Mobile.Ruqi.Common;
using FlyingFish.Mobile.Ruqi.Dtos;

using Hybrid.AspNetCore.UI;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer4.Web.Services
{
    /// <summary>
    /// 飞鱼服务接口
    /// </summary>
    public interface IFlyingFishService
    {
        Task<AjaxResult<object>> CheckUserAsync(RQBaseRequest request);

        Task<AjaxResult> PushPhaseOneAsync(RQBaseRequest request);

        Task<AjaxResult> PushPhaseTwoAsync(RQBaseRequest request);

        Task<AjaxResult<List<RQBankInfo>>> BankListAsync(RQBankListRequest request);

        Task<AjaxResult<RQCardListResponse>> CardListAsync(RQBaseRequest request);

        Task<AjaxResult<RQBindCardResponse>> BindCardAsync(RQBaseRequest request);

        Task<AjaxResult<BindCardExtResponse>> BindCardExtAsync(RQBaseRequest request);

        Task<AjaxResult<RQWithdrawTryCalculateResponse>> WithdrawTryCalculateAsync(RQBaseRequest request);

        Task<AjaxResult> ConfirmLoanAsync(RQBaseRequest request);

        Task<AjaxResult<RQLoanContractExtResponse>> LoanContractExtAsync(RQBaseRequest request);

        Task<AjaxResult<RQPullOrderStatusResponse>> PullOrderStatusAsync(RQBaseRequest request);

        Task<AjaxResult<DepotAuthorizeResponse>> DepotAuthorizeAsync(RQBaseRequest request);

        Task<AjaxResult<DepotWithdrawResponse>> DepotWithdrawAsync(RQBaseRequest request);

        Task<AjaxResult<RQRepaymentResponse>> RepaymentAsync(RQBaseRequest request);

        Task<AjaxResult<RepaymentExtResponse>> RepaymentExtAsync(RQBaseRequest request);

        Task<AjaxResult<AuthorizeExtResponse>> AuthorizeExtAsync(AuthorizeExtRequest request);
    }
}