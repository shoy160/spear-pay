using System.Net.Http;

namespace SpearPay.Gateway
{
    /// <inheritdoc />
    /// <summary> 支付网关基类 </summary>
    public abstract class BaseGateway : IGateway
    {
        protected readonly IHttpClientFactory HttpClientFactory;
        private BaseGateway(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            Data = new GatewayData();
        }

        protected BaseGateway(IHttpClientFactory httpClientFactory, IMerchant merchant) : this(httpClientFactory)
        {
            Merchant = merchant;
        }

        public IMerchant Merchant { get; set; }

        /// <summary> 支付网关 </summary>
        protected abstract string Gateway { get; set; }

        /// <summary> 网关数据 </summary>
        protected GatewayData Data { get; set; }

        public virtual TResponse Request<TResponse, TModel>(IRequest<TModel> request)
            where TResponse : IResponse
            where TModel : class
        {
            Data.Add(request.Model);
            //:todo 
            var client = HttpClientFactory.CreateClient();
            var req = new HttpRequestMessage(HttpMethod.Post, request.Url)
            {

            };
            var resp = client.SendAsync(req);

            return default(TResponse);
        }
    }
}
