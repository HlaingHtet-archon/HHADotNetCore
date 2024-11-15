using BankPay.Database.Models;
using BankPay.Domain.features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPay.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly DepositService _service = new DepositService();

        [HttpPost]
        public IActionResult CreateDeposit(string mobileNumber, decimal Balance)
        {
            var model = _service.CreateDeposit(mobileNumber, Balance);
            return Ok(model);
        }

    }
}
