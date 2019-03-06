using SpearPay.Naming;
using System.ComponentModel.DataAnnotations;


namespace SpearPay.AllinPay
{
    /// <summary> 通联支付商户信息 </summary>
    public class Merchant : IMerchant
    {
        /// <summary> 平台分配的APPID </summary>
        [Required(ErrorMessage = "AppId不能为空")]
        [Naming("appid")]
        public string AppId { get; set; }

        [Naming("notifyurl")]
        public string NotifyUrl { get; set; }
        /// <summary> 平台分配的商户号 </summary>
        [Required(ErrorMessage = "商户号不能为空")]
        [Naming("cusid")]
        public string CusId { get; set; }

        [Ignore]
        [Required(ErrorMessage = "密钥不能为空")]
        public string Key { get; set; }

        [Naming("charset")]
        public string Charset => "UTF-8";
    }
}
