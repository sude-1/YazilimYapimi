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
        IDataResult<List<Product>> GetAllByCategory(int id); //ürünleri kategoriye göre getirme veri tabanından olacağı için IDataResult
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max); //ürünleri fiyata göre listeleme
        IDataResult<List<ProductDetailDto>> GetProductDetails(); //ürünün detayına gitme
        IDataResult<Product> GetById(int productId); // ürün id sine göre getirme
        IDataResult<Product> IsThereAnyProduct(AddProduct addProduct);//ürün var mı 
        IResult Update(Product product); //güncelleme
        IResult UpdateList(List<Product> products); //güncellenen listeyi getirme
        IResult Add(Product product);//sadece mesaj
        IDataResult<List<Product>> GetByName(string productName, int supplierId); //ürün ismi ve satıcıya göre ürün listeleme

    }
}
