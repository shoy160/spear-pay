using SpearPay.AllinPay.Models;
using SpearPay.AllinPay.Response;

namespace SpearPay.AllinPay.Request
{
    public class RefundRequest : DRequest<RefundModel, RefundResponse>
    {
        public RefundRequest(RefundModel model)
        {
            Url = "refund";
            AddModel(model);
        }

        public sealed override void AddModel(RefundModel model)
        {
            base.AddModel(model);
            Data.Add(model);
        }
    }
}
