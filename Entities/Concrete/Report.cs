using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Report : IEntity
    {
        public DateTime Date { get; set; }
        public string Product { get; set; }
        public decimal Sum { get; set; }
        public decimal Quantity { get; set; }
    }
}
