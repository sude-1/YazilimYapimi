using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{

    //burada döviz sistemi apimizi json formatında çalıştırtım ve gerekli özellikleri yazdım
    public class MoneyValues
    {
        public Rates Rates { get; set; }
    }
    public class Rates
    {
        public decimal TRY { get; set; }
    }
}
