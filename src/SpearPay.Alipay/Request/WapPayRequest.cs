using SpearPay.Alipay.Models;
using SpearPay.Alipay.Response;

namespace SpearPay.Alipay.Request
{
    public class WapPayRequest : AlipayRequest<WapPayModel, WapPayResponse>
    {
        public WapPayRequest(WapPayModel model)
            : base("alipay.trade.wap.pay", model) { }
    }
}
