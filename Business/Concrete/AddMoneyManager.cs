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
            // onaylama
            //döviz apim
            string adres = $"https://api.exchangerate.host/latest?base={addMoney.CurrencyUnit}&symbols=TRY&amount={addMoney.Money}";
            var data = await WebApiHelper.GetMethod<MoneyValues>(adres);

            var result = _addMoneyDal.Get(a => a.Id == addMoney.Id); 
            if (result.Confirmation)
            {
                return new ErrorResult();
            }
            result.Confirmation = true; //para onayı true 
            _addMoneyDal.Update(result);//güncelleme yapılıyor
            
            //kullanıcının cüzdanın para ekleme yapılacak kullanıcı id ve paarayı türk lirasına çevirip ekleme yapılıyor
            _userWalletService.AddMoney(new UserWallet { UserId = result.UserId, Money = data.Rates.TRY });
            return new SuccessResult();
        }

        [SecuredOperation("admin")] //yetkisi admin ise
        [CacheAspect]
        public IDataResult<List<AddMoneyDetailDto>> GetApproved()
        {
            //burada onaylanacak paraların listesini getiriyor
            return new SuccessDataResult<List<AddMoneyDetailDto>>
                (_addMoneyDal.GetAddMoneyDetails(addmoney => addmoney.Confirmation == false));
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
