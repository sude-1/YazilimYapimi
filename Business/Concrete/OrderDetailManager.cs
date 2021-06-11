using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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

        public IResult Add(OrderDetail orderDetail)
        {
            
            _orderDetailDal.Add(orderDetail);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        public IDataResult<List<OrderDetailDto>> GetOrderDetails()
        {
            return new SuccessDataResult<List<OrderDetailDto>>(_orderDetailDal.GetOrderDetails());
        }
    }
}
