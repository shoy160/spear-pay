using Newtonsoft.Json;

namespace SpearPay.Alipay.Request
{
    public abstract class AlipayRequest<TModel, TResponse> : DRequest<TModel, TResponse>
        where TModel : IModel
        where TResponse : IResponse
    {
        protected AlipayRequest(string method)
        {
            Url = "/gateway.do?charset=UTF-8";
            Data.Add("method", method);
        }

        protected AlipayRequest(string method, TModel model) : this(method)
        {
            AddModel(model);
        }

        public sealed override void AddModel(TModel model)
        {
            base.AddModel(model);
            var content = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
            Data.Add("biz_content", content);
        }
    }
}
