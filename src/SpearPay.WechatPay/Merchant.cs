namespace SpearPay.WechatPay
{
    public class Merchant : IMerchant
    {
        public string AppId { get; set; }
        public string NotifyUrl { get; set; }
    }
}
