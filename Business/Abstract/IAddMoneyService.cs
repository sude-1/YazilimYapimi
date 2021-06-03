using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAddMoneyService 
    {
        IResult Add(AddMoney money);
        IDataResult<List<AddMoneyDetailDto>> GetApproved();
        Task<IResult> Approve(AddMoneyDetailDto addMoney);
        IResult Refusal(int addMoneyId);
    }
}

