using SpearPay.Naming;
using System;

namespace SpearPay.AllinPay.Models
{
    public abstract class BaseModel : IModel
    {
        protected BaseModel()
        {
            RandomStr = Guid.NewGuid().ToString("N");
        }

        /// <summary> 随机字符串 </summary>
        [Naming("randomstr")]
        public string RandomStr { get; set; }
    }
}
