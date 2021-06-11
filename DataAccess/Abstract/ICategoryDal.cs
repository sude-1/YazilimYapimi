using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {      
        // dto olmadığı için bir şey yazmıyorum belki ilerleyen zamanlarda olur diye oluşturmam gerekli 
    }
}
