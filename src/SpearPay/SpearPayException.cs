using System;

namespace SpearPay
{
    public class SpearPayException : Exception
    {
        public SpearPayException(string message)
            : base(message)
        {
        }
    }
}
