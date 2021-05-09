using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAddMoneyDal : EfEntityRepositoryBase<AddMoney, YazilimYapimiContext>, IAddMoneyDal
    {
        public List<AddMoneyDetailDto> GetAddMoneyDetails(Expression<Func<AddMoney, bool>> filter = null)
        {
            using (YazilimYapimiContext context = new YazilimYapimiContext())
            {
                var result =
                from addmoney in filter == null ? context.AddMoney : context.AddMoney.Where(filter)
                join u in context.Users
                on addmoney.UserId equals u.Id
                select new AddMoneyDetailDto
                {
                    Id = addmoney.Id,
                    Money = addmoney.Money,
                    Confirmation = addmoney.Confirmation,
                    UserName = u.UserName
                };
                return result.ToList();
            }
        }
    }
}
