using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidatonRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
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
        IProductService _productService;

        public AddProductManager(IAddProductDal addProductDal,IProductService productService)
        {
            _addProductDal = addProductDal;
            _productService = productService; 
        }

        [ValidationAspect(typeof(AddProductValidator))]
        [SecuredOperation("user,admin")]
        [CacheRemoveAspect("IAddProduct.Get")]
        public IResult Add(AddProduct product)
        {
            _addProductDal.Add(product);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IAddProduct.Get")]
        public IResult Approve(int addproductId)
        {
            var result = _addProductDal.Get(addp => addp.Id == addproductId);
            if (result.Confirmation)
            {
                return new ErrorResult();
            }
            result.Confirmation = true;
            var product = _productService.IsThereAnyProduct(result).Data;
            if (product!=null)
            {
                product.Quantity += result.Quantity;
                _productService.Update(product);
            }
            else
            {
                _productService.Add(new Product
                {
                    Quantity = result.Quantity,
                    ProductName = result.ProductName,
                    UnitPrice = result.UnitPrice,
                    SupplierId = result.SupplierId,
                    CategoryId = result.CategoryId
                });
            }
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        [CacheAspect]
        public IDataResult<List<AddProductDetailDto>> ToBeApproved()//onaylancaklar
        {
            return new SuccessDataResult<List<AddProductDetailDto>>
                (_addProductDal.GetAddProductDetails(addp => addp.Confirmation == false));
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IAddProduct.Get")]
        public IResult Refusal(int addproductId)
        {
            _addProductDal.Delete(new AddProduct { Id = addproductId });
            return new SuccessResult("ürün ekleme reddedildi");
        }
    }
}

