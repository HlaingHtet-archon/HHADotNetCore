using BankPay.Database.Models;
using BankPay.Domain.features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPay.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController()
        {
            _service = new UserService();
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var lst = _service.GetUsers();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var item = _service.GetUser(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateUser(TblUser user)
        {
            var model = _service.CreateUser(user);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, TblUser user)
        {
            var item = _service.UpdateUser(id, user);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, TblUser user)
        {
            var item = _service.PatchUser(id, user);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpDelete("{int}")]
        public IActionResult DeleteUser(int id)
        {
            var item = _service.DeleteUser(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
