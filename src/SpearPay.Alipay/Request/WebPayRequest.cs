using SpearPay.Alipay.Models;
using SpearPay.Alipay.Response;

namespace SpearPay.Alipay.Request
{
    /// <summary> 网页支付 </summary>
    public class WebPayRequest : AlipayRequest<WebPayModel, WebPayResponse>
    {
        public WebPayRequest(WebPayModel model)
            : base("alipay.trade.page.pay", model)
        {
        }
    }
}
