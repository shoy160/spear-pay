using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SpearPay.Alipay.Response
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class AlipayResponse : IResponse
    {
        /// <summary>
        /// 网关返回码,详见文档
        /// https://docs.open.alipay.com/common/105806
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 网关返回码描述,详见文档
        /// https://docs.open.alipay.com/common/105806
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

        /// <summary>
        /// 网关返回码,详见文档
        /// https://docs.open.alipay.com/common/105806
        /// </summary>
        public string SubCode { get; set; }

        /// <summary>
        /// 网关返回码描述,详见文档
        /// https://docs.open.alipay.com/common/105806
        /// </summary>
        [JsonProperty("sub_msg")]
        public string SubMessage { get; set; }

        /// <summary> 签名 </summary>
        public string Sign { get; set; }

        /// <summary> 原始值 </summary>
        public string Raw { get; set; }

        internal virtual void Execute<TModel, TResponse>(Merchant merchant, IRequest<TModel, TResponse> request)
            where TResponse : IResponse
            where TModel : IModel
        {

        }
    }
}
