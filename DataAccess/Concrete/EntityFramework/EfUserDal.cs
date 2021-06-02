using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, YazilimYapimiContext>, IUserDal
    {
        public void AddUserOperationClaims(UserOperationClaim userOperationClaim)
        {
            using (var context = new YazilimYapimiContext())
            {
                context.UserOperationClaims.Add(userOperationClaim);
                context.SaveChanges();
            }
        }

        public User GetAccountant()
        {
            using(var context = new YazilimYapimiContext())
            {
                var result = from accountant in context.UserOperationClaims //muhasebe değişkenim kullanıcının işlem talepleri tablosu ile bağlıyor 
                             join user in context.Users on accountant.UserId equals user.Id // user tablosuna join atıyoruz ve muhasebecinin userıd'si ile user'ın ıd kısmını eşitliyoruz 
                             where accountant.OperationClaimId == 1002 // muhasebecinin tealep işlemi 1002 ise onu seçiyoruz
                             select user;
                return result.FirstOrDefault(); // sorgudan bir tane veri döndürür
            }
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new YazilimYapimiContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
    }
}
