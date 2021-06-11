using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class ProductDetailDto : IDto
    {
        //ürünün detayları veri tabanımda kategori ismi yerine id tutuyorum ama burada yazmak istediğim özellikler data access katmanında başka tablolara join atarak elde edebilirim
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public string SupplierName { get; set; }
    }
}
