using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpearPay.AllinPay;
using SpearPay.AllinPay.Models;
using SpearPay.AllinPay.Request;
using System.Threading.Tasks;

namespace SpearPay.Tests
{
    [TestClass]
    public class AllinPayTest : BaseTest
    {
        protected override void MapServices(IServiceCollection services)
        {
            var merchant = Config.GetSection("payment:allinpay").Get<Merchant>();
            services.AddGateway<AllinPayGateway>(merchant);
            base.MapServices(services);
        }

        [TestMethod]
        public async Task WebPayTest()
        {
            var gateway = GetService<IGateway>();
            var request = new WebPayRequest(new WebPayModel
            {
                Amount = 100,
                OrderId = "D001",
                ReturnUrl = "http://www.baidu.com",
                GoodsId = "10001",
                GoodsInfo = "招投标承保系统保费"
            });
            var resp = await gateway.Execute(request);
            Print(resp);
        }

        [TestMethod]
        public async Task QueryTest()
        {
            var gateway = GetService<IGateway>();
            var request = new QueryRequest(new QueryModel
            {
                OrderId = "D20190306001",
                TradeId = null
            });
            var resp = await gateway.Execute(request);
            Print(resp);
        }

        [TestMethod]
        public async Task RefundTest()
        {
            var gateway = GetService<IGateway>();
            var request = new RefundRequest(new RefundModel
            {
                Amount = 1,
                OrderId = "D20190306001",
                TradeId = null,
                ReqSn = "R20190306001"
            });
            var resp = await gateway.Execute(request);
            Print(resp);
        }

    }
}
