using SpearPay.Alipay.Request;

namespace SpearPay.Alipay.Response
{
    public class AppPayResponse : IResponse
    {
        public AppPayResponse(AppPayRequest request)
        {
            OrderInfo = request.Data.ToUrl();
        }
        /// <summary>
        /// 用于唤起App的订单参数
        /// </summary>
        public string OrderInfo { get; set; }

        public string Raw { get; set; }
    }
}
