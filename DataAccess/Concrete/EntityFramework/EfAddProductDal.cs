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
                    from addproduct in filter == null ? context.AddProducts : context.AddProducts.Where(filter)
                    join c in context.Categories 
                    on addproduct.CategoryId equals c.CategoryId
                    join u in context.Users
                    on addproduct.SupplierId equals u.Id
                    // neye göre join etmesi gerektiğini belirttim 
                    select new AddProductDetailDto 
                    {
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

