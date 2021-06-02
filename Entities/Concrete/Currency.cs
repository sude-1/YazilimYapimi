using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Currency : IEntity // veri tabanında bir tablo olduğu için
    {
        public int CurrencyId { get; set; }
        public string CurrencyUnit { get; set; }
    }
}
