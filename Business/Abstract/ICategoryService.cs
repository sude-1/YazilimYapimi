using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll(); //kategorilerin hepsini getirme 
        IDataResult<Category> GetById(int categoryId); // kategori id sine göre getirme
    }
}
