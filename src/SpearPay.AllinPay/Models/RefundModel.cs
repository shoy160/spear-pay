using System.ComponentModel.DataAnnotations;
using SpearPay.Naming;

namespace SpearPay.AllinPay.Models
{
    /// <summary> 退款模型 </summary>
    public class RefundModel : QueryModel
    {
        /// <summary> 商户退款流水 </summary>
        [Naming("reqsn")]
        [Required(ErrorMessage = "商户退款流水不能为空")]
        public string ReqSn { get; set; }

        /// <summary> 付款金额(单位为分) </summary>
        [Naming("trxamt")]
        [Required(ErrorMessage = "请设置退款金额")]
        public long Amount { get; set; }
    }
}
