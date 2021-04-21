using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, YazilimYapimiContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (YazilimYapimiContext context = new YazilimYapimiContext())
            {
                var result = from p in context.Products // ürünlere p dedim
                             join c in context.Categories // kategorilere c dedim ürünlerle kategorilere join at demek
                             on p.CategoryId equals c.CategoryId
                             join u in context.Users
                             on p.SupplierId equals u.Id
                             // neye göre join etmesi gerektiğini belirttim 
                             select new ProductDetailDto // sonucu productdetaildto nun içinde olan kolonlara uygun olarak ver
                             {
                                 CategoryId = p.CategoryId,
                                 CategoryName = c.CategoryName,
                                 UnitPrice = p.UnitPrice,
                                 Quantity = p.Quantity,
                                 ProductName = p.ProductName,
                                 SupplierName = u.UserName
                             };
                return result.ToList(); // sonucu liste haline getir ve döndür var result döngü türü(IQueryable (LİNQ for database))
            }
        }
    }
}
