﻿using Business.Abstract;
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
    public class AdminController : ControllerBase
    {
        IAddProductService _addProductService;
        IAddMoneyService _addMoneyService;

        public AdminController(IAddMoneyService addMoneyService, IAddProductService addProductService)
        {
            _addMoneyService = addMoneyService;
            _addProductService = addProductService;
        }

        [HttpGet("addmoney")]

        public IActionResult AddMoney(AddMoney money)
        {
            var result = _addMoneyService.Add(money);//sor salihe
            if (result.Success)
            {
                return Ok(result);
            }           
            return BadRequest(result);           
        }

        [HttpPost("approveaddmoney")]//para ekleme onaylama
        public IActionResult ApproveAddMoney(int addMoneyId)
        {
            var result = _addMoneyService.Approve(addMoneyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("refusaladdmoney")] // para reddetme 
        public IActionResult RefusalAddMoney(int addMoneyId)
        {
            var result = _addMoneyService.Refusal(addMoneyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("addproduct")]
        public IActionResult AddProduct(AddProduct product)
        {
            var result = _addProductService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("approveaddproduct")]//ürün ekleme
        public IActionResult ApproveAddProduct(int addproductId)
        {
            var result = _addProductService.Approve(addproductId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("refusaladdproduct")]//ürün reddetme
        public IActionResult RefusalAddProduct(int addproductId)
        {
            var result = _addProductService.Refusal(addproductId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
