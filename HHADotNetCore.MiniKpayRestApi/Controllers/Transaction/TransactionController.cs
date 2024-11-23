using HHADotNetCore.MiniKpayDomain.features.transaction;
using HHADotNetCore.MiniKpayDomain.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HHADotNetCore.MiniKpayRestApi.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private readonly TransactionService _service;

        public TransactionController(TransactionService service)
        {
            _service = service;
        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer(TransferRequestModel transferRequestModel)
        {
            var model = await _service.TransferAsync(
                transferRequestModel.SenderId, transferRequestModel.ReceiverId, transferRequestModel.Amount, transferRequestModel.PinCode);
            return Execute(model);
        }

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw(TransactionRequestModel transactionRequestModel)
        {
            var model = await _service.WithdrawAsync(
                transactionRequestModel.UserId, transactionRequestModel.Amount, transactionRequestModel.PinCode);
            return Execute(model);
        }

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(TransactionRequestModel transactionRequestModel)
        {
            var model = await _service.DepositAsync(
                transactionRequestModel.UserId, transactionRequestModel.Amount);
            return Execute(model);
        }
    }
}
