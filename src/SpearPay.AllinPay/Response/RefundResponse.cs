namespace SpearPay.AllinPay.Response
{
    public class RefundResponse : BaseResponse
    {
        /// <summary> 商户退款流水 </summary>
        public string reqsn { get; set; }
        /// <summary> 交易单号 </summary>
        public string trxid { get; set; }
        /// <summary> 交易状态 </summary>
        public string trxstatus { get; set; }
        /// <summary> 错误原因 </summary>
        public string errmsg { get; set; }
        /// <summary> 随机字符串 </summary>
        public string randomstr { get; set; }
    }
}
