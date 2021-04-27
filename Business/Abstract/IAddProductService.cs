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
        IResult Add(Product product);
        IDataResult<List<AddProduct>> ToBeApproved();
        IDataResult<AddProduct> GetByIdToApprove(int productId);
        IDataResult<List<AddProductDetailDto>> GetAddProductDetails();

    }
}
