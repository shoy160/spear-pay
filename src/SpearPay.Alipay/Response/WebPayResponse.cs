using System.Net;
using SpearPay.Alipay.Request;

namespace SpearPay.Alipay.Response
{
    public class WebPayResponse : IResponse
    {
        public WebPayResponse(WebPayRequest request)
        {
            Html = request.Data.ToForm(request.Url);
            Raw = request.Data.Raw;
        }

        /// <summary>
        /// 生成的Html网页
        /// </summary>
        public string Html { get; set; }

        public string Raw { get; set; }
    }
}
