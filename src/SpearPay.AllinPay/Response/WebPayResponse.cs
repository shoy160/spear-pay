using SpearPay.AllinPay.Request;

namespace SpearPay.AllinPay.Response
{
    public class WebPayResponse : IResponse
    {
        public WebPayResponse(WebPayRequest request)
        {
            Html = request.Data.ToForm(request.Url);
            Raw = request.Data.Raw;
        }
        /// <summary> 生成的Html网页 </summary>
        public string Html { get; set; }
        public string Raw { get; set; }
    }
}
