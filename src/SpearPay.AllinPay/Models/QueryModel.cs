using SpearPay.Naming;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpearPay.AllinPay.Models
{
    /// <summary> 查询模型 </summary>
    public class QueryModel : BaseModel, IValidatableObject
    {
        /// <summary> 商户唯一订单号(订单号码支持数字、英文字母、_、-、*、+、#，其他字符不建议使用) </summary>
        [Naming("orderid")]
        public string OrderId { get; set; }

        /// <summary> 平台交易流水 </summary>
        [Naming("trxid")]
        public string TradeId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(OrderId) && string.IsNullOrWhiteSpace(TradeId))
                yield return new ValidationResult("商户订单号和平台交易流水不能同时为空");
            yield return ValidationResult.Success;
        }
    }
}
