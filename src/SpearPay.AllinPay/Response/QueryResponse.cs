namespace SpearPay.AllinPay.Response
{
    public class QueryResponse : BaseResponse
    {
        /// <summary> 交易备注(返回商品号,goodsid) </summary>
        public string trxreserved { get; set; }

        /// <summary> 平台的交易流水号 </summary>
        public string trxid { get; set; }

        /// <summary> 交易类型 </summary>
        public string trxcode { get; set; }

        /// <summary> 交易金额(单位为分) </summary>
        public long trxamt { get; set; }

        /// <summary> 交易状态 </summary>
        public string trxstatus { get; set; }

        /// <summary> 交易完成时间(yyyyMMddHHmmss) </summary>
        public string fintime { get; set; }

        /// <summary> 随机字符串 </summary>
        public string randomstr { get; set; }

        /// <summary> 错误原因 </summary>
        public string errmsg { get; set; }
    }
}
