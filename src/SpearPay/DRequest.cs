using SpearPay.Gateway;
using SpearPay.Naming;

namespace SpearPay
{
    public abstract class DRequest<TModel, TResponse> : IRequest<TModel, TResponse>
        where TModel : IModel
        where TResponse : IResponse
    {
        protected DRequest()
        {
            Data = new RequestData();
        }

        /// <inheritdoc />
        /// <summary> 请求地址(只读) </summary>
        public string Url { get; set; }
        /// <inheritdoc />
        /// <summary> 异步回调地址 </summary>
        public string NotifyUrl { get; set; }
        /// <inheritdoc />
        /// <summary> 同步回调地址 </summary>
        public string ReturnUrl { get; set; }

        /// <summary> 请求模型 </summary>
        public TModel Model { get; private set; }
        /// <summary> 请求数据 </summary>
        public RequestData Data { get; }

        /// <summary> 添加请求模型 </summary>
        /// <param name="model"></param>
        public virtual void AddModel(TModel model)
        {
            model.Validate();
            Model = model;
        }
    }
}
