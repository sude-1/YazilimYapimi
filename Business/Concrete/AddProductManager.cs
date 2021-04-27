using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AddProductManager : IAddProductService
    {
        IAddProductDal _addProductDal;

        public AddProductManager(IAddProductDal addProductDal)
        {
            _addProductDal = addProductDal;
        }

        public IResult Add(Product product)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<AddProductDetailDto>> GetAddProductDetails()
        {
            throw new NotImplementedException();
        }

        public IDataResult<AddProduct> GetByIdToApprove(int productId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<AddProduct>> ToBeApproved()//onaylancaklar
        {
            return new SuccessDataResult<List<AddProduct>>(_addProductDal.GetAll(p => p.Confirmation == false));
        }
    }
}
