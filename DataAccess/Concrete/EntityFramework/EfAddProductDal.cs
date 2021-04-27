using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAddProductDal : EfEntityRepositoryBase<AddProduct, YazilimYapimiContext>, IAddProductDal
    {
        public List<AddProductDetailDto> GetAddProductDetails()
        {
            using (YazilimYapimiContext context = new YazilimYapimiContext())
            {
                
            }
        }
    }
}

//return result.ToList(); // sonucu liste hali