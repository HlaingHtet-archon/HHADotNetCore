using BankPay.Domain.features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPay.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly TransactionHistoryService _service = new TransactionHistoryService();

        [HttpGet]
        public IActionResult GetTransactionHistory(string mobileNumber)
        {
            var model = _service.GetTransactionHistory(mobileNumber); 
            return Ok(model); 
        }

    }
}
