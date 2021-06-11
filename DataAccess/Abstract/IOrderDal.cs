using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepository<Order>
    {
        // dto olmadığı için bir şey yazmıyorum belki ilerleyen zamanlarda olur diye oluşturmam gerekli 
    }
}
