using SpearPay.Gateway;
using System;
using System.Threading.Tasks;

namespace SpearPay
{
    /// <summary> 支付网关接口 </summary>
    public interface IGateway
    {
        IServiceProvider Provider { get; }
        /// <summary> 商户数据 </summary>
        IMerchant Merchant { get; }

        /// <summary> 数据请求 </summary>
        Task<TResponse> Execute<TResponse, TModel>(IRequest<TModel, TResponse> request)
            where TResponse : IResponse
            where TModel : IModel;

        bool VerifySign(RequestData data, string sign);
    }
}
