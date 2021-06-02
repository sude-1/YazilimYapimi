using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IUserWalletService _userWalletService;

        public UserManager(IUserDal userDal,IUserWalletService userWalletService)
        {
            _userDal = userDal;
            _userWalletService = userWalletService;
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            _userDal.AddUserOperationClaims(new UserOperationClaim { UserId = user.Id, OperationClaimId = 2 });
            _userWalletService.CreateWallet(user);
            return new SuccessResult();
        }

        public IDataResult<User> GetAccountant()
        {
            return new SuccessDataResult<User>(_userDal.GetAccountant());
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }
    }
}