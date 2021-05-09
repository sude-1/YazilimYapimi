using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class OrderDetailDto :IDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }

    }
}
