using SpearPay.AllinPay.Models;
using SpearPay.AllinPay.Response;

namespace SpearPay.AllinPay.Request
{
    public class WebPayRequest : DRequest<WebPayModel, WebPayResponse>
    {
        public WebPayRequest(WebPayModel model)
        {
            Url = "pay";
            AddModel(model);
        }

        public sealed override void AddModel(WebPayModel model)
        {
            base.AddModel(model);
            Data.Add(model);
        }
    }
}
