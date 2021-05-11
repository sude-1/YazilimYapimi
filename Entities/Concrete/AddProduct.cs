using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class AddProduct : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public bool  Confirmation { get; set; }
        public string ProductName { get; set; }
    }
}
