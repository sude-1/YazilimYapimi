using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class OrderDetail : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int OrderId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
