using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CurrencyManager : ICurrencyService
    {
        ICurrencyDal _currencyDal;

        public CurrencyManager(ICurrencyDal currencyDal)
        {
            _currencyDal = currencyDal;
        }
        public IDataResult<List<Currency>> GetAllCurrency()
        {
            //bütün hepsini getiriyor
            return new SuccessDataResult<List<Currency>>(_currencyDal.GetAll());
        }
    }
}
