using FlyingFish.Mobile.Ruqi.Common;
using FlyingFish.Mobile.Ruqi.Dtos;

using Hybrid.AspNetCore.UI;
using Hybrid.Data;
using Hybrid.Dependency;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using XC.RSAUtil;

namespace IdentityServer4.Web.Services
{
    public sealed class FlyingFishService : IFlyingFishService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RsaPkcs1Util _frRsaPkcs1Util;

        public FlyingFishService(IHttpClientFactory httpClientFactory, SingletonFactory singletonFactory)
        {
            _httpClientFactory = httpClientFactory;
            _frRsaPkcs1Util = singletonFactory.GetService<RsaPkcs1Util>("FlyingFishAndRuqiRSA");
        }

        public Task<AjaxResult<AuthorizeExtResponse>> AuthorizeExtAsync(AuthorizeExtRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AjaxResult<List<RQBankInfo>>> BankListAsync(RQBankListRequest rQBankListRequest)
        {
            AjaxResult<List<RQBankInfo>> ajaxResult = new AjaxResult<List<RQBankInfo>>();
            string bizData = JsonConvert.SerializeObject(rQBankListRequest);

            //实例化
            RQBaseRequest rQBaseRequest = new RQBaseRequest();
            rQBaseRequest.bizData = bizData;
            SortedDictionary<string, string> singKeyValuePairs = new SortedDictionary<string, string>
                    {
                        { "appId", rQBaseRequest.appId },
                        { "signType", rQBaseRequest.signType },
                        { "version", rQBaseRequest.version },
                        { "timestamp", rQBaseRequest.timestamp },
                        { "bizData", bizData }
                    };
            string signParamsStr = OrdinalComparer.FormatParam(singKeyValuePairs);

            //请求如期签名
            string sign = _frRsaPkcs1Util.SignData(signParamsStr, HashAlgorithmName.MD5, RSASignaturePadding.Pkcs1);
            rQBaseRequest.sign = sign;

            var request = new HttpRequestMessage(HttpMethod.Post, RuqiConsts.Urls.BankList);

            HttpClient httpClient = _httpClientFactory.CreateClient(RuqiConsts.RuqiHttpClient);

            string contentStr = JsonConvert.SerializeObject(rQBaseRequest);
            request.Content = new StringContent(contentStr, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                ajaxResult.Success = false;
                ajaxResult.Content = "接口请求失败，请重试！";
            }
            else
            {
                string result = await response.Content.ReadAsStringAsync();
                //TODO:InsertAsync
                //ServiceParams rqParams = new ServiceParams
                //{
                //    JsonContext = result,
                //    ServiceType = (int)ServiceType.BankList,
                //    ServiceName = "BankList",
                //    AppId = "30002"
                //};

                //await _repository.InsertAsync(rqParams);
                var obj = JsonConvert.DeserializeObject<BaseResult<List<RQBankInfo>>>(result);

                if (obj.code.Equals(200))
                {
                    ajaxResult.Result = obj.data;
                }
                else
                {
                    ajaxResult.Success = false;
                    ajaxResult.Content = "请求失败，请重试！";
                }
            }
            return ajaxResult;
        }

        public Task<AjaxResult<RQBindCardResponse>> BindCardAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<BindCardExtResponse>> BindCardExtAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<RQCardListResponse>> CardListAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<object>> CheckUserAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult> ConfirmLoanAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<DepotAuthorizeResponse>> DepotAuthorizeAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<DepotWithdrawResponse>> DepotWithdrawAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<RQLoanContractExtResponse>> LoanContractExtAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<RQPullOrderStatusResponse>> PullOrderStatusAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult> PushPhaseOneAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult> PushPhaseTwoAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<RQRepaymentResponse>> RepaymentAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<RepaymentExtResponse>> RepaymentExtAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult<RQWithdrawTryCalculateResponse>> WithdrawTryCalculateAsync(RQBaseRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
