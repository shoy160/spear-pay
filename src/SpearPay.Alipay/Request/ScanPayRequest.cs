using SpearPay.Alipay.Models;
using SpearPay.Alipay.Response;

namespace SpearPay.Alipay.Request
{
    public class ScanPayRequest : AlipayRequest<ScanPayModel, ScanPayResponse>
    {
        public ScanPayRequest(ScanPayModel model)
            : base("alipay.trade.precreate", model)
        {
        }
    }
}
