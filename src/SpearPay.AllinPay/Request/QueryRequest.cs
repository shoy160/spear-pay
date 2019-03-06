using SpearPay.AllinPay.Models;
using SpearPay.AllinPay.Response;

namespace SpearPay.AllinPay.Request
{
    public class QueryRequest : DRequest<QueryModel, QueryResponse>
    {
        public QueryRequest(QueryModel model)
        {
            Url = "query";
            AddModel(model);
        }

        public sealed override void AddModel(QueryModel model)
        {
            base.AddModel(model);
            Data.Add(model);
        }
    }
}
