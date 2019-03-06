using System.Collections.Generic;

namespace SpearPay.AllinPay
{
    public static class Constants
    {
        public static Dictionary<string, string> TrxCodes = new Dictionary<string, string>
        {
            {"VSP531", "网关支付"},
            {"VSP533", "网关支付退款"},
            {"VSP534", "网关B2B支付"}
        };

        public static Dictionary<string, string> TrxStatus = new Dictionary<string, string>
        {
            {"0000", "交易成功或交易已受理"},
            {"2000", "交易处理中"},
            {"2008", "交易处理中"},
            {"3044", "交易超时"},
            {"3008", "余额不足"},
            {"3999", "交易失败"}
        };

        public static string TrxCodeDesc(this string code)
        {
            if (TrxCodes.ContainsKey(code))
                return TrxCodes[code];
            return "交易类型异常";
        }

        public static string TrxStatusDesc(this string status)
        {
            if (TrxStatus.ContainsKey(status))
                return TrxCodes[status];
            return "交易状态异常";
        }
    }
}
