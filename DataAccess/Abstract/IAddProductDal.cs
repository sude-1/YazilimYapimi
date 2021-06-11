using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IAddProductDal : IEntityRepository<AddProduct>
    {
        //AddProductDetailDto yu listeliyoruz ve bunu filtre verebiliriz diyoruz
        List<AddProductDetailDto> GetAddProductDetails(Expression<Func<AddProduct, bool>> filter = null);
    }
}
