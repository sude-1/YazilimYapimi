using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidatonRules.FluentValidation
{
    public class AddMoneyValidator : AbstractValidator<AddMoney>
    {
        public AddMoneyValidator()
        {
            RuleFor(a => a.UserId).NotEmpty();
            RuleFor(a => a.Money).GreaterThan(0);
            RuleFor(a => a.Money).NotEmpty();
        }
    }
}
