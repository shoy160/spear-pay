using SpearPay.Alipay.Request;

namespace SpearPay.Alipay.Response
{
    public class WapPayResponse : IResponse
    {
        public WapPayResponse(WapPayRequest request)
        {
            Url = $"{request.Url}&{request.Data.ToUrl()}";
        }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string Url { get; set; }

        public string Raw { get; set; }
    }
}
