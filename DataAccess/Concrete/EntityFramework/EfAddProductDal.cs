using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAddProductDal : EfEntityRepositoryBase<AddProduct, YazilimYapimiContext>, IAddProductDal
    {
        // onaylanan veya onaylanmayan ürünler için filtre
        public List<AddProductDetailDto> GetAddProductDetails(Expression<Func<AddProduct, bool>> filter = null)
        {
            using (YazilimYapimiContext context = new YazilimYapimiContext())
            {
                var result =
                    //addproduct diye bir değişken oluşturuyoruz filte null mı kontrolü yapıyoruz null ise direk tablo ile bağlanırken null değilken filtreyi uyguluyoruz
                    from addproduct in filter == null ? context.AddProducts : context.AddProducts.Where(filter)
                    join c in context.Categories // c diye değişken oluşturup kategori tablomla bağlıyorum
                    on addproduct.CategoryId equals c.CategoryId // ürün ve kategori tablomdaki kategori id leri eşitliyorum
                    join u in context.Users // u diye değişken oluşturup users tabloma bağlıyorum
                    on addproduct.SupplierId equals u.Id // burada satıcı id lerini eşitliyorum
                    // neye göre join etmesi gerektiğini belirttim 
                    select new AddProductDetailDto 
                    {
                        // AddProductDetailDto da olan özelliklerimi nereden alacağını belirtiyorum
                        AddProductId = addproduct.Id,
                        CategoryName = c.CategoryName,
                        UnitPrice = addproduct.UnitPrice,
                        Quantity = addproduct.Quantity,
                        ProductName = addproduct.ProductName,
                        SupplierName = u.UserName
                    };
                return result.ToList(); 
            }
        }
    }
}

