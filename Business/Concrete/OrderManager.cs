using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidatonRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        IOrderDetailService _orderDetailService;
        IUserWalletService _userWalletService;
        IProductService _productService;

        public OrderManager(IOrderDal orderDal, IOrderDetailService orderDetailService, 
            IUserWalletService userWalletService, IProductService productService)
        {
            _orderDal = orderDal;
            _orderDetailService = orderDetailService;
            _productService = productService;
            _userWalletService = userWalletService;
        }

        [ValidationAspect(typeof(OrderValidator))]
        [SecuredOperation("user")]
        [CacheRemoveAspect("IOrderService.Get")]
        public IResult Add(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
