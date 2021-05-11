using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        //ilgili kullanıcı için o kullanıcının claimlerini içerecek bir token üretecek
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
