using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class AddMoneyDetailDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal Money { get; set; }
        public bool Confirmation { get; set; }
        public string CurrencyUnit { get; set; }
    }
}
