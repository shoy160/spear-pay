namespace SpearPay
{
    /// <summary> 商户信息接口 </summary>
    public interface IMerchant
    {
        /// <summary> 应用ID </summary>
        string AppId { get; set; }

        /// <summary> 异步回调地址 </summary>
        string NotifyUrl { get; set; }
    }
}
