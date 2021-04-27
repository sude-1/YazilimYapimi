using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class AddMoney : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Money { get; set; }
        public bool Confirmation { get; set; }
    }
}
