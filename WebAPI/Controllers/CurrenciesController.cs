using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : Controller
    {
        ICurrencyService _currencyService;
        public CurrenciesController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _currencyService.GetAllCurrency();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
