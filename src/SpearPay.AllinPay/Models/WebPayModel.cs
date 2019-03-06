using SpearPay.Naming;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpearPay.AllinPay.Models
{
    public class WebPayModel : BaseModel
    {
        public WebPayModel()
        {
            RandomStr = Guid.NewGuid().ToString("N");
        }

        /// <summary> 页面跳转同步通知页面路径 </summary>
        [Naming("returl")]
        public string ReturnUrl { get; set; }

        /// <summary> 商品号 </summary>
        [Naming("goodid")]
        public string GoodsId { get; set; }

        /// <summary> 商品描述信息 </summary>
        [Naming("goodsinf")]
        public string GoodsInfo { get; set; }

        /// <summary> 付款金额(单位为分) </summary>
        [Required(ErrorMessage = "请设置支付金额")]
        [Naming("trxamt")]
        public long Amount { get; set; }

        /// <summary> 商户唯一订单号(订单号码支持数字、英文字母、_、-、*、+、#，其他字符不建议使用) </summary>
        [Required(ErrorMessage = "商户唯一订单号不能为空")]
        [Naming("orderid")]
        public string OrderId { get; set; }

        /// <summary> 随机字符串 </summary>
        [Naming("randomstr")]
        public string RandomStr { get; set; }

        /// <summary> 支付银行(不填时，将在网关平台显示银行列表供用户选择，https://aipboss.allinpay.com/know/devhelp/main.php?pid=13) </summary>
        [Naming("gateid")]
        public string GateId { get; set; }

        /// <summary> 有效时间(订单有效时间，以分为单位，不填默认为720分钟(12小时)) </summary>
        [Naming("validtime")]
        public int ValidTime { get; set; } = 720;

        /// <summary> 交易类型B2C/B2B，默认B2B </summary>
        [Naming("paytype")]
        public string PayType { get; set; } = "B2B";
    }
}
