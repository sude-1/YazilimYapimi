using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class MoneyValues
    {
        public Rates Rates { get; set; }
    }
    public class Rates
    {
        public decimal TRY { get; set; }
    }
}
