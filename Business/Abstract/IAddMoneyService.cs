using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAddMoneyService 
    {
        IResult Add(AddMoney money);
        IDataResult<List<AddMoneyDetailDto>> GetApproved();
        IResult Approve(int addMoneyId);
        IResult Refusal(int addMoneyId);
    }
}

