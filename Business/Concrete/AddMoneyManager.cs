using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidatonRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AddMoneyManager : IAddMoneyService
    {
        IAddMoneyDal _addMoneyDal;
        IUserWalletService _userWalletService;

        public AddMoneyManager(IAddMoneyDal addMoneyDal, IUserWalletService userWalletService)
        {
            _addMoneyDal = addMoneyDal;
            _userWalletService = userWalletService;
        }
        
        [SecuredOperation("user")]
        [ValidationAspect(typeof(AddMoneyValidator))]
        [CacheRemoveAspect("IAddMoneyService.Get")]
        public IResult Add(AddMoney money)
        {
            //ürün ekleme
            _addMoneyDal.Add(money);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IAddMoneyService.Get")]
        public async Task<IResult> Approve(AddMoneyDetailDto addMoney)
        {
            //ürün onaylama
            string adres = $"https://api.exchangerate.host/latest?base={addMoney.CurrencyUnit}&symbols=TRY&amount={addMoney.Money}";
            //var myType = MyTypeBuilder.CompileResultType(new List<Field>() { new Field { FieldName = addMoney.CurrencyUnit, FieldType = typeof(MoneyValues) } });
            var data = await WebApiHelper.GetMethod<MoneyValues>(adres);

           // MoneyValues money = data.GetType().GetProperty(addMoney.CurrencyUnit).GetValue(data) as MoneyValues;
            var result = _addMoneyDal.Get(a => a.Id == addMoney.Id);
            if (result.Confirmation)
            {
                return new ErrorResult();
            }
            result.Confirmation = true;
            _addMoneyDal.Update(result);
            
            _userWalletService.AddMoney(new UserWallet { UserId = result.UserId, Money = data.Rates.TRY });
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        [CacheAspect]
        public IDataResult<List<AddMoneyDetailDto>> GetApproved()
        {
            return new SuccessDataResult<List<AddMoneyDetailDto>>
                (_addMoneyDal.GetAddMoneyDetails(addmoney => addmoney.Confirmation == false));
            //onaylancakları getiriyor
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IAddMoneyService.Get")]
        public IResult Refusal(int addMoneyId)
        {
            _addMoneyDal.Delete(new AddMoney { Id = addMoneyId }); //para eklemeyi reddetme
            return new SuccessResult("para ekleme işlemi reddedildi");
        }
    }
}
