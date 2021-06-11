using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidatonRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //iş kodları yazılır

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategory(int id)
        {
            //girilen ürünün kategori id si ile diğer ürünün kategori id si aynı mı aynıysa getir
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByName(string productName, int supplierId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.ProductName.Contains(productName.Trim())&&p.SupplierId!=supplierId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            //ürününün fiyatı girilen min değerinden büyük mü ve max değerinden kücük mü uygun olanaları listele 
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        public IDataResult<Product> IsThereAnyProduct(AddProduct addProduct)
        {
            //daha önce veri tabanımızda aynı ürün var mı kontrolü yapıyorum eklenenen ürünün kateori id sine satıcısına fiyat ve ismine bakıyoruz 
            return new SuccessDataResult<Product>(_productDal.Get(p => p.CategoryId == addProduct.CategoryId &&
             p.SupplierId==addProduct.SupplierId 
            && p.Quantity==addProduct.Quantity && p.ProductName==addProduct.ProductName ));
        }

        [CacheRemoveAspect("IProductService.Get")]
        [SecuredOperation("user")]
        public IResult Update(Product product)
        {
            //güncelleme 
            _productDal.Update(product);

            return new SuccessResult(Messages.ProductUpdated);
        }

        public IResult UpdateList(List<Product> products)
        {
            _productDal.UpdateList(products);

            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}