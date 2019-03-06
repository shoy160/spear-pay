using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpearPay.Alipay.Response;
using SpearPay.Gateway;
using SpearPay.Helper;
using SpearPay.Naming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpearPay.Alipay
{
    public class AlipayGateway : BaseGateway
    {
        private readonly Merchant _merchant;
        public AlipayGateway(IServiceProvider provider, Merchant merchant)
            : base(provider, merchant)
        {
            _merchant = merchant;
        }

        protected override string Gateway => "https://openapi.alipay.com";

        protected override void AddMerchantData<TResponse, TModel>(IRequest<TModel, TResponse> request)
        {
            request.Data.Add(Merchant, NamingCase.Snake);

            
            if (!string.IsNullOrEmpty(request.NotifyUrl))
            {
                request.Data.Add("notify_url", request.NotifyUrl);
            }
            if (!string.IsNullOrEmpty(request.ReturnUrl))
            {
                request.Data.Add("return_url", request.ReturnUrl);
            }
            //签名
            Logger.LogDebug("begin sign");
            request.Data.Add("sign", BuildSign(request.Data, _merchant.Privatekey, _merchant.SignType));
        }

        protected override async Task<TResponse> InternalExecute<TResponse, TModel>(IRequest<TModel, TResponse> request)
        {
            if (!typeof(AlipayResponse).IsAssignableFrom(typeof(TResponse)))
                return (TResponse)Activator.CreateInstance(typeof(TResponse), request);
            var client = Provider.GetService<IHttpClientFactory>().CreateClient();
            var content = new FormUrlEncodedContent(request.Data.Select(t =>
                new KeyValuePair<string, string>(t.Key, t.Value.ToString())));
            var resp = await client.PostAsync(request.Url, content);
            if (!resp.IsSuccessStatusCode)
                throw new SpearPayException($"网络请求异常:{resp.StatusCode}");
            var result = await resp.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(result);
            var jToken = jObject.First.First;
            var sign = jObject.Value<string>("sign");
            if (!string.IsNullOrEmpty(sign) &&
                !CheckSign(jToken.ToString(Formatting.None), sign, _merchant.AlipayPublicKey, _merchant.SignType))
            {
                throw new SpearPayException("签名验证失败");
            }
            var baseResponse = (AlipayResponse)jToken.ToObject(typeof(TResponse));
            baseResponse.Raw = result;
            baseResponse.Sign = sign;
            baseResponse.Execute(_merchant, request);
            return (TResponse)(object)baseResponse;

        }

        internal string BuildSign(RequestData data, string privatekey, string signType)
        {
            data.Remove("sign");
            var unsigned = data.ToUrl(false);
            var signed = EncryptHelper.Rsa(unsigned, privatekey, signType);
            Logger.LogDebug($"signType:{signType} ->{Environment.NewLine} un_sign:{unsigned} {Environment.NewLine} signed:{signed}");
            return signed;
        }

        internal static bool CheckSign(string data, string sign, string alipayPublicKey, string signType)
        {
            var result = EncryptHelper.RSAVerifyData(data, sign, alipayPublicKey, signType);
            if (result) return true;
            data = data.Replace("/", "\\/");
            result = EncryptHelper.RSAVerifyData(data, sign, alipayPublicKey, signType);
            return result;
        }
    }
}
