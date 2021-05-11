using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserWalletManager : IUserWalletService
    {
        IUserWalletDal _userWalletDal;

        public UserWalletManager(IUserWalletDal userWalletDal)
        {
            _userWalletDal = userWalletDal;
        }

        [SecuredOperation("user")]
        public IResult AddMoney(UserWallet userWallet)
        {
            var result = _userWalletDal.Get(uw => uw.UserId == userWallet.UserId);
            result.Money += userWallet.Money;
            _userWalletDal.Update(result);
            return new SuccessResult();
        }

        public IResult CreateWallet(User user)
        {
            _userWalletDal.Add(new UserWallet { UserId = user.Id, Money = 0 });
            return new SuccessResult();
        }

        [SecuredOperation("user")]
        public IDataResult<UserWallet> GetById(int userId)
        {
            return new SuccessDataResult<UserWallet>(_userWalletDal.Get(u => u.UserId == userId));
        }
    }
}
