using BankPay.Domain.features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPay.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly TransferService _service = new TransferService();

        [HttpPost]
        public IActionResult Transfer(string senderMobileNumber, string receiverMobileNumber, decimal amount, string pin)
        {
            var result = _service.CreateTransfer(senderMobileNumber, receiverMobileNumber, amount, pin);

            return Ok(result);
        }
    }
}
