using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace SpearPay.Gateway
{
    /// <inheritdoc />
    /// <summary> 支付网关基类 </summary>
    public abstract class BaseGateway : IGateway
    {
        protected readonly ILogger Logger;
        protected BaseGateway(IServiceProvider provider, IMerchant merchant)
        {
            Logger = provider.GetService<ILoggerFactory>().CreateLogger(GetType());
            Provider = provider;
            Merchant = merchant;
        }

        public IServiceProvider Provider { get; }

        public IMerchant Merchant { get; set; }

        /// <summary> 支付网关 </summary>
        protected abstract string Gateway { get; }

        /// <summary> 添加商户信息以及签名 </summary>
        protected abstract void AddMerchantData<TResponse, TModel>(IRequest<TModel, TResponse> request)
            where TResponse : IResponse
            where TModel : IModel;

        /// <summary> 添加商户信息以及签名 </summary>
        protected abstract Task<TResponse> InternalExecute<TResponse, TModel>(IRequest<TModel, TResponse> request)
            where TResponse : IResponse
            where TModel : IModel;

        public virtual Task<TResponse> Execute<TResponse, TModel>(IRequest<TModel, TResponse> request)
            where TResponse : IResponse
            where TModel : IModel
        {
            request.Url = new Uri(new Uri(Gateway), request.Url).AbsoluteUri;
            AddMerchantData(request);
            return InternalExecute(request);
        }

        public abstract bool VerifySign(RequestData data, string sign);
    }
}
