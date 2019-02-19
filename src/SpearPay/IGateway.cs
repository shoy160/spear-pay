namespace SpearPay
{
    /// <summary> 支付网关接口 </summary>
    public interface IGateway
    {
        /// <summary> 商户数据 </summary>
        IMerchant Merchant { get; set; }

        /// <summary> 数据请求 </summary>
        TResponse Request<TResponse, TModel>(IRequest<TModel> request)
            where TResponse : IResponse
            where TModel : class;
    }
}
