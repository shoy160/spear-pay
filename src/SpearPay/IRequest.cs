using SpearPay.Gateway;

namespace SpearPay
{
    /// <summary> 请求参数基础接口 </summary>
    public interface IModel { }

    public interface IRequest<TModel, TResponse>
        where TModel : IModel
        where TResponse : IResponse
    {
        /// <summary> 请求地址 </summary>
        string Url { get; set; }

        /// <summary> 异步回调地址 </summary>
        string NotifyUrl { get; set; }

        /// <summary> 同步通知地址 </summary>
        string ReturnUrl { get; set; }

        TModel Model { get; }

        RequestData Data { get; }

        void AddModel(TModel model);
    }
}
