namespace SpearPay
{
    public interface IRequest<T> where T : class
    {
        /// <summary> 请求地址 </summary>
        string Url { get; set; }
        /// <summary> 异步回调地址 </summary>
        string NotifyUrl { get; set; }
        /// <summary> 同步通知地址 </summary>
        string ReturnUrl { get; set; }
        /// <summary> 请求的具体数据 </summary>
        T Model { get; set; }
    }
}
