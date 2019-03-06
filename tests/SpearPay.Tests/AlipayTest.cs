using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpearPay.Alipay;
using SpearPay.Alipay.Models;
using SpearPay.Alipay.Request;
using System.Threading.Tasks;
using SpearPay.Gateway;

namespace SpearPay.Tests
{
    [TestClass]
    public class AlipayTest : BaseTest
    {
        protected override void MapServices(IServiceCollection services)
        {
            var merchant = Config.GetSection("payment:alipay").Get<Merchant>();
            services.AddGateway<AlipayGateway>(merchant);
            base.MapServices(services);
        }

        [TestMethod]
        public async Task WebPayTest()
        {
            var gateway = GetService<IGateway>();
            var request = new WebPayRequest(new WebPayModel
            {
                OutTradeNo = "abc001",
                TotalAmount = 0.01,
                Subject = "spear_pay",
                Body = "test"
            });
            var resp = await gateway.Execute(request);
            Print(resp);
        }

        [TestMethod]
        public async Task AppPayTest()
        {
            var gateway = GetService<IGateway>();
            var request = new AppPayRequest(new AppPayModel
            {
                OutTradeNo = "abc001",
                TotalAmount = 0.01,
                Subject = "spear_pay",
                Body = "test"
            });
            var resp = await gateway.Execute(request);
            Print(resp);
        }

        [TestMethod]
        public async Task ScanPayTest()
        {
            var gateway = GetService<IGateway>();
            var request = new ScanPayRequest(new ScanPayModel
            {
                OutTradeNo = "abc001",
                TotalAmount = 0.01,
                Subject = "spear_pay",
                Body = "test"
            });
            var resp = await gateway.Execute(request);
            Print(resp);
        }
    }
}
