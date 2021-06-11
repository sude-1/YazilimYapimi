using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        //GetClaims fonksiyonunu kullanarak OperationClaim i listeleme yapıyorum burada User tipinde bir değişken oluşturuyorum
        List<OperationClaim> GetClaims(User user);
        void AddUserOperationClaims(UserOperationClaim userOperationClaim);
        User GetAccountant(); // muhasebeci için
    }
}
