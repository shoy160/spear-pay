using Microsoft.AspNetCore.Mvc;
using SpearPay.AllinPay.Models;
using SpearPay.AllinPay.Request;
using System.Threading.Tasks;

namespace SpearPay.WebTest.Controllers
{
    [Route("payment")]
    public class PaymentController : Controller
    {
        private readonly IGateway _gateway;

        public PaymentController(IGateway gateway)
        {
            _gateway = gateway;
        }

        [HttpGet("web")]
        public async Task<IActionResult> Web(string orderNo, int amount, string bank = "")
        {
            var request = new WebPayRequest(new WebPayModel
            {
                Amount = amount,
                OrderId = orderNo,
                ReturnUrl = "http://www.baidu.com",
                GateId = bank,
                PayType = "B2B"
            });
            var result = await _gateway.Execute(request);
            return Content(result.Html, "text/html");
        }
    }
}
