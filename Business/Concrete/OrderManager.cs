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
using System.Linq;

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
       // [SecuredOperation("user")]
        [CacheRemoveAspect("IOrderService.Get")]
        public IResult Add(Order order)
        {
            var result = _productService.GetByName(order.ProductName,order.CustomerId).Data.OrderBy(p=>p.UnitPrice);
            var customerWallet = _userWalletService.GetById(order.CustomerId).Data;
            var orderquantity = order.Quantity; //bu kadar ürün almak istiyorum 
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            
            foreach (var product in result)
            {
                var canbuy = product.Quantity > orderquantity ? (orderquantity ) : (product.Quantity );//alabilceğim ürün
                var purchased = customerWallet.Money >= canbuy * product.UnitPrice ? canbuy : (customerWallet.Money / product.UnitPrice);//verilen ürün
                orderquantity -= purchased;

                if (purchased > 0)
                {
                    var supplierWallet = _userWalletService.GetById(product.SupplierId).Data;
                    customerWallet.Money -= purchased * product.UnitPrice;
                    supplierWallet.Money += purchased * product.UnitPrice;
                    _userWalletService.Update(supplierWallet);
                    _userWalletService.Update(customerWallet);
                    product.Quantity -= purchased;
                    _productService.Update(product);
                    orderDetails.Add(new OrderDetail { ProductId=product.ProductId,Quantity=purchased});
                }
               

                if(orderquantity==0)
                {
                    break;
                }
            }
            if (orderquantity != order.Quantity)
            {
                order.Quantity -= orderquantity;
                order.OrderDate = DateTime.Now;
                _orderDal.Add(order);
                foreach (var item in orderDetails)
                {
                    item.OrderId = order.OrderId;
                    _orderDetailService.Add(item);
                }
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}

