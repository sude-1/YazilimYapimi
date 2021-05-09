using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();//hem veri hem mesaj döndürcek
        IDataResult<List<Product>> GetAllByCategory(int id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId);
        IDataResult<Product> IsThereAnyProduct(AddProduct addProduct);//ürün var mı 
        IResult Update(Product product);
        IResult Add(Product product);//sadece mesaj
        //IResult AddTransactionalTest(Product product);


    }
}
