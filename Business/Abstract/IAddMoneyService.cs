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
        IResult Add(AddMoney money); // para ekleme
        IDataResult<List<AddMoneyDetailDto>> GetApproved(); //onaylancak paraların listesini getirme 
        Task<IResult> Approve(AddMoneyDetailDto addMoney);//onay işlemi yapılıyor
        IResult Refusal(int addMoneyId); //reddetme işlemi
    }
}

