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
        //EfAddMoneyDal implemente oluyor IAddMoneyDal dan ve oradai fonksiyonu kullanıyor
        public List<AddMoneyDetailDto> GetAddMoneyDetails(Expression<Func<AddMoney, bool>> filter = null)
        {
            using (YazilimYapimiContext context = new YazilimYapimiContext()) //veritabanımız ile bağlıyoruz
            {

                var result =
                //addmoney diye bir değişken oluşturuyoruz filte null mı kontrolü yapıyoruz null ise direk tablo ile bağlanırken null değilken filtreyi uyguluyoruz
                from addmoney in filter == null ? context.AddMoney : context.AddMoney.Where(filter)
                join u in context.Users on addmoney.UserId equals u.Id //Users tablomdaki Id ile AddMoney tablomdaki Userıd yi eşitliyorum
                join c in context.Currencies on addmoney.CurrencyId equals c.CurrencyId //burada para birimlerini eşiyleme yapıyorum
                select new AddMoneyDetailDto
                {
                    // AddMoneyDetailDto da olan özelliklerimi nereden alacağını belirtiyorum
                    Id = addmoney.Id, 
                    Money = addmoney.Money,
                    Confirmation = addmoney.Confirmation,
                    UserName = u.UserName,
                    CurrencyUnit = c.CurrencyUnit
                };
                return result.ToList();
            }
        }
    }
}
