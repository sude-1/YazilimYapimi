using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAddProductService
    {
        IResult Add(AddProduct product); //ürün ekleme
        IDataResult<List<AddProductDetailDto>> ToBeApproved(); //onaylanacak listesi
        //IDataResult<List<AddProductDetailDto>> GetAddProductDetails();
        IResult Refusal(int addproductId); //reddedilme
        IResult Approve(int addproductId);//id üzerinden onaylama
    }
}
