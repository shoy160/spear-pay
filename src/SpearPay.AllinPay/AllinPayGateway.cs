using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpearPay.AllinPay.Request;
using SpearPay.AllinPay.Response;
using SpearPay.Exception;
using SpearPay.Gateway;
using SpearPay.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpearPay.AllinPay
{
    public class AllinPayGateway : BaseGateway
    {
        public AllinPayGateway(IServiceProvider provider, IMerchant merchant)
            : base(provider, merchant)
        {
        }

        protected override string Gateway => "https://vsp.allinpay.com/apiweb/gateway/";

        protected string SignData(RequestData data, string key)
        {
            data.Add("key", key);
            data.Remove("sign");
            var unsigned = data.ToUrl(false, true);
            Logger.LogDebug($"unsigned:{unsigned}");
            var signed = EncryptHelper.Md5(unsigned).ToUpper();
            data.Remove("key");
            return signed;
        }

        public override bool VerifySign(RequestData data, string sign)
        {
            data.Remove("sign");
            data.Add("key", (Merchant as Merchant)?.Key);
            var unsigned = data.ToUrl(false, true);
            Logger.LogDebug($"unsigned:{unsigned}");
            var signed = EncryptHelper.Md5(unsigned);
            return string.Equals(signed, sign, StringComparison.CurrentCultureIgnoreCase);
        }

        protected override void AddMerchantData<TResponse, TModel>(IRequest<TModel, TResponse> request)
        {
            if (Merchant != null)
                request.Data.Add(Merchant);
            if (request is QueryRequest)
            {
                request.Data.Remove("notifyurl");
            }

            if (request is QueryRequest || request is RefundRequest)
            {
                request.Data.Remove("charset");
            }

            if (!string.IsNullOrEmpty(request.NotifyUrl))
            {
                request.Data.Add("notifyurl", request.NotifyUrl);
            }
            if (!string.IsNullOrEmpty(request.ReturnUrl))
            {
                request.Data.Add("returl", request.ReturnUrl);
            }
            //签名
            Logger.LogDebug("begin sign");
            request.Data.Add("sign", SignData(request.Data, (Merchant as Merchant)?.Key));
        }

        protected override async Task<TResponse> InternalExecute<TResponse, TModel>(IRequest<TModel, TResponse> request)
        {
            if (request is WebPayRequest)
            {
                return (TResponse)Activator.CreateInstance(typeof(TResponse), request);
            }
            var client = Provider.GetService<IHttpClientFactory>().CreateClient();
            var content = new FormUrlEncodedContent(request.Data.Select(t =>
                new KeyValuePair<string, string>(t.Key, t.Value.ToString())));
            Logger.LogInformation(JsonConvert.SerializeObject(request.Data));
            var resp = await client.PostAsync(request.Url, content);
            if (!resp.IsSuccessStatusCode)
                throw new SpearPayException($"网络请求异常:{resp.StatusCode}");
            var result = await resp.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(result);
            var sign = jObject.Value<string>("sign");
            var data = new RequestData();
            data.FromJson(result);
            if (!string.IsNullOrEmpty(sign) &&
                !VerifySign(data, sign))
            {
                throw new SpearPayException("签名验证失败");
            }
            var baseResponse = (BaseResponse)jObject.ToObject(typeof(TResponse));
            baseResponse.Raw = result;
            baseResponse.sign = sign;
            return (TResponse)(object)baseResponse;
        }
    }
}
