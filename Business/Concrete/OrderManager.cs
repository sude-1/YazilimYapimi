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
        IUserService _userService;

        public OrderManager(IOrderDal orderDal, IOrderDetailService orderDetailService,
            IUserWalletService userWalletService, IProductService productService, IUserService userService)
        {
            _orderDal = orderDal;
            _orderDetailService = orderDetailService;
            _productService = productService;
            _userWalletService = userWalletService;
            _userService = userService;
        }

        [ValidationAspect(typeof(OrderValidator))]
        // [SecuredOperation("user")]
        [CacheRemoveAspect("IOrderService.Get")]
        public IResult Add(Order order)
        {
            var result = _productService.GetByName(order.ProductName, order.CustomerId).Data.Where(p => p.UnitPrice == order.UnitPrice);
            var customerWallet = _userWalletService.GetById(order.CustomerId).Data;
            var accountantWallet = _userWalletService.GetById(_userService.GetAccountant().Data.Id);
            var orderquantity = order.Quantity; //bu kadar ürün almak istiyorum 
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            List<UserWallet> supplierWallets = new List<UserWallet>();
            List<Product> matchingProducts = new List<Product>();

            foreach (var product in result)
            {
                var canbuy = product.Quantity > orderquantity ? (orderquantity) : (product.Quantity);//alabilceğim ürün
                var purchased = customerWallet.Money >= canbuy * product.UnitPrice ? canbuy : (customerWallet.Money / product.UnitPrice);//verilen ürün
                orderquantity -= purchased;

                if (purchased > 0)
                {
                    var supplierWallet = _userWalletService.GetById(product.SupplierId).Data;
                    customerWallet.Money -= purchased * product.UnitPrice;
                    supplierWallet.Money += purchased * product.UnitPrice;
                    accountantWallet.Data.Money += purchased * product.UnitPrice / 100;
                    supplierWallets.Add(supplierWallet);                   
                    product.Quantity -= purchased;
                    matchingProducts.Add(product);
                    orderDetails.Add(new OrderDetail { ProductId = product.ProductId, Quantity = purchased, SupplierId = product.SupplierId, OrderDate=DateTime.Now});
                }

                if (orderquantity == 0)
                {

                    _userWalletService.Update(customerWallet);
                    _userWalletService.Update(accountantWallet.Data);
                    _userWalletService.UpdateList(supplierWallets);
                    _productService.UpdateList(matchingProducts);
                    if (order.OrderId > 0) // order var demek
                    {
                        order.OrderPending = false;
                        _orderDal.Update(order);
                    }
                    else
                    {
                        order.OrderDate = DateTime.Now;
                        _orderDal.Add(order);
                    }

                    foreach (var item in orderDetails)
                    {
                        item.OrderId = order.OrderId;
                        _orderDetailService.Add(item);
                    }
                    return new SuccessResult();
                }
            }
          
            order.OrderPending = true;
            if (order.OrderId > 0) // order var demek
            {
                _orderDal.Update(order);
            }
            else
            {
                order.OrderDate = DateTime.Now;
                _orderDal.Add(order);
            }
            return new ErrorResult();
        }

        public IDataResult<List<Order>> GetByProductNameOrderPending(Product productName)
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(o => o.OrderPending == true && o.ProductName == productName.ProductName));
        }

        public IDataResult<List<Order>> GetByReport(ReportDetail reportDetail)
        {
            var result = _orderDal.GetAll(o => o.CustomerId == reportDetail.UserId && o.OrderDate > reportDetail.StartDate
             && o.OrderDate < reportDetail.EndDate && o.OrderPending == false);
            return new SuccessDataResult<List<Order>>(result);
        }
    }


}

