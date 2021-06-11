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
        IOrderService _orderService;

        //constructor oluşturuyoruz hep new yapmamak için ilk önce bu kısım çalışır
        public AddProductManager(IAddProductDal addProductDal,IProductService productService, IOrderService orderService)
        {
            _addProductDal = addProductDal;
            _productService = productService;
            _orderService = orderService;
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
            //onaylancakları id ye göre geteiriyoruz
            var result = _addProductDal.Get(addp => addp.Id == addproductId);
            if (result.Confirmation)
            {
                return new ErrorResult(); //onaylanmışşsa tekrar onaylanmasın
            }
            result.Confirmation = true; //onaylanmamışsa true yap onayı 
            var product = _productService.IsThereAnyProduct(result).Data; // hiç ürün var mı fonksiyonundaki data yı getiriyorouz
            if (product!=null) //ürün var ise
            {
                product.Quantity += result.Quantity; //o ürünün kilosounu artıyorum
                _productService.Update(product); // güncelleme yapıyorum
            }
            else
            { // yok ise yeni bir ürün oluşturuyorum 
                var p = new Product
                {
                    Quantity = result.Quantity,
                    ProductName = result.ProductName,
                    UnitPrice = result.UnitPrice,
                    SupplierId = result.SupplierId,
                    CategoryId = result.CategoryId
                };
                _productService.Add(p); //yeni ürünü ekliyorum
                foreach (var order in _orderService.GetByProductNameOrderPending(p).Data) //bekleyen ürün var mı
                {
                    _orderService.Add(order); 
                }
            }
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        [CacheAspect]
        public IDataResult<List<AddProductDetailDto>> ToBeApproved()//onaylancaklar
        {
            //onaylanacak ürünleri listeleme
            return new SuccessDataResult<List<AddProductDetailDto>>
                (_addProductDal.GetAddProductDetails(addp => addp.Confirmation == false));
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IAddProduct.Get")]
        public IResult Refusal(int addproductId)
        {
            //reddetme işlemi
            _addProductDal.Delete(new AddProduct { Id = addproductId });
            return new SuccessResult("ürün ekleme reddedildi");
        }
    }
}

