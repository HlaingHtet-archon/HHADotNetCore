using BankPay.Domain.features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPay.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawController : ControllerBase
    {
        private readonly WithdrawService _service = new WithdrawService();

        [HttpPost]
        public IActionResult CreateWithdraw(string mobileNumber, decimal Balance)
        {
            var result = _service.CreateWithdraw(mobileNumber, Balance);
            return Ok(result);
        }

    }
}
