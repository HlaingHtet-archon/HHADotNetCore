using HHADotNetCore.MiniKpayDatabase.Models;
using HHADotNetCore.MiniKpayDomain.features.transaction;
using HHADotNetCore.MiniKpayDomain.features.user;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HHADotNetCore.MiniKpayRestApi.Controllers.user
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletUserController : BaseController
    {
        private readonly WalletUserService _service;

        public WalletUserController(WalletUserService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(TblWalletUser CreatedUser)
        {
            var user = await _service.CreateUserAsync(CreatedUser);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _service.GetUserAsync(id);
            return Ok(user);
        }

        [HttpPatch("ChangePin/{id}")]
        public async Task<IActionResult> ChangePin(int id, TblWalletUser newPin)
        {
            var item = await _service.ChangePinAsync(id, newPin);
            return Ok(item);
        }

        [HttpPatch("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, TblWalletUser newUser)
        {
            var user = await _service.UpdateUserAsync(id, newUser);
            return Ok(user);
        }
    }
}
