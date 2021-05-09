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
        IResult Add(AddProduct product);
        IDataResult<List<AddProductDetailDto>> ToBeApproved();
        //IDataResult<List<AddProductDetailDto>> GetAddProductDetails();
        IResult Refusal(int addproductId);
        IResult Approve(int addproductId);//id üzerinden onaylama
    }
}
