namespace SpearPay.AllinPay.Response
{
    public class BaseResponse : IResponse
    {
        public bool Success => retcode == "SUCCESS";
        /// <summary> 返回码(SUCCESS/FAIL) </summary>
        public string retcode { get; set; }
        /// <summary> 返回码说明 </summary>
        public string retmsg { get; set; }
        /// <summary> 商户号 </summary>
        public string cusid { get; set; }
        /// <summary> 应用ID </summary>
        public string appid { get; set; }
        /// <summary> 商户订单号 </summary>
        public string orderid { get; set; }
        /// <summary> 签名 </summary>
        public string sign { get; set; }
        /// <summary> 原始数据 </summary>
        public string Raw { get; set; }
    }
}
