using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersWalletsController : ControllerBase
    {
        IUserWalletService _userWalletService;
        IAddMoneyService _addMoneyService;
        public UsersWalletsController(IUserWalletService userWalletService, IAddMoneyService addMoneyService)
        {
            _addMoneyService = addMoneyService;
            _userWalletService = userWalletService;
        }

        [HttpPost("add")]
        public IActionResult Add(AddMoney money)
        {
            var result = _addMoneyService.Add(money);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int userId)
        {
            var result = _userWalletService.GetById(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
