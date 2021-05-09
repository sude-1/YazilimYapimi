using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidatonRules.FluentValidation
{
    public class OrderValidator :AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(a => a.CustomerId).NotEmpty();
            RuleFor(a => a.CategoryId).NotEmpty();
            RuleFor(a => a.Quantity).NotEmpty();
        }
    }
}
