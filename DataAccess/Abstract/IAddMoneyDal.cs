using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IAddMoneyDal : IEntityRepository<AddMoney>
    {
        //AddMoneyDetailDto yu listeliyoruz ve bunu filtre verebiliriz diyoruz
        List<AddMoneyDetailDto> GetAddMoneyDetails(Expression<Func<AddMoney, bool>> filter = null);
    }
}
