using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OrderDetailManager : IOrderDetailService
    {
        //entity framework e direk bağlamadım burada gevşek bağlılık kullandım çünkü değişim kolay olsun
        IOrderDetailDal _orderDetailDal;

        //constructor kullanma nedenim arka tarafta direk new'lenir.
        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }
        public IDataResult<List<OrderDetailDto>> GetOrderDetails()
        {
            return new SuccessDataResult<List<OrderDetailDto>>(_orderDetailDal.GetOrderDetails());
        }
    }
}
