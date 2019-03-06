using SpearPay.Alipay.Models;
using SpearPay.Alipay.Response;

namespace SpearPay.Alipay.Request
{
    public class AppPayRequest : AlipayRequest<AppPayModel, AppPayResponse>
    {
        public AppPayRequest(AppPayModel model)
            : base("alipay.trade.app.pay", model)
        {
        }
    }
}
