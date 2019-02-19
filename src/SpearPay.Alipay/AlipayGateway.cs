using System.Net.Http;
using System.Threading.Tasks;
using SpearPay.Gateway;

namespace SpearPay.Alipay
{
    public class AlipayGateway : BaseGateway
    {
        public AlipayGateway(IHttpClientFactory httpClientFactory, Merchant merchant)
            : base(httpClientFactory, merchant)
        {
        }

        protected override string Gateway { get; set; } = "https://openapi.alipay.com";
    }
}
