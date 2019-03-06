namespace SpearPay.AllinPay.Response
{
    public class NotifyResponse : IResponse
    {
        /// <summary> 平台分配的APPID </summary>
        public string appid { get; set; }

        /// <summary> 第三方交易号 </summary>
        public string outtrxid { get; set; }

        /// <summary> 交易类型 </summary>
        public string trxcode { get; set; }

        /// <summary> 交易流水号 </summary>
        public string trxid { get; set; }

        /// <summary> 金额(分) </summary>
        public long trxamt { get; set; }

        /// <summary> 交易请求日期(yyyymmdd) </summary>
        public string trxdate { get; set; }

        /// <summary> 交易完成时间(yyyymmddhhmmss) </summary>
        public string paytime { get; set; }

        /// <summary> 渠道流水号 </summary>
        public string chnltrxid { get; set; }

        /// <summary> 交易结果码 </summary>
        public string trxstatus { get; set; }

        /// <summary> 商户号 </summary>
        public string cusid { get; set; }

        /// <summary> 终端编码 </summary>
        public string termno { get; set; }

        /// <summary> 终端批次号 </summary>
        public string termbatchid { get; set; }

        /// <summary> 终端流水 </summary>
        public string termtraceno { get; set; }

        /// <summary> 终端授权码 </summary>
        public string termauthno { get; set; }

        /// <summary> 终端参考号 </summary>
        public string termrefnum { get; set; }

        /// <summary> 业务关联内容 </summary>
        public string trxreserved { get; set; }

        /// <summary> 原交易流水 </summary>
        public string srctrxid { get; set; }

        /// <summary> 业务流水 </summary>
        public string cusorderid { get; set; }

        /// <summary> 交易帐号 </summary>
        public string acct { get; set; }

        /// <summary> 手续费(分) </summary>
        public long fee { get; set; }

        /// <summary> 签名类型(MD5或RSA) </summary>
        public string signtype { get; set; }

        /// <summary> 签名 </summary>
        public string sign { get; set; }

        public string Raw { get; set; }
    }
}
