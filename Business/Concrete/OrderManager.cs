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
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Order order)
        {
            //sipariş ismi ve müsşteri id ye bakılıyor ürünün fiyatı ile girilen fiyat uygun mu 
            var result = _productService.GetByName(order.ProductName, order.CustomerId).Data.Where(p => p.UnitPrice == order.UnitPrice);
            var customerWallet = _userWalletService.GetById(order.CustomerId).Data;//müşteri cüzdanının verileri çekiliyor
            var accountantWallet = _userWalletService.GetById(_userService.GetAccountant().Data.Id);//muhasebecinin cüzdanının verileri çekiliyor
            var orderquantity = order.Quantity; //bu kadar ürün almak istiyorum 
            List<OrderDetail> orderDetails = new List<OrderDetail>(); //sipariş detayı oluşturma
            List<UserWallet> supplierWallets = new List<UserWallet>(); //satıcı cüzdanı oluşturma
            List<Product> matchingProducts = new List<Product>();//eşleşen ürün oluşturma

            foreach (var product in result)
            {
                var canbuy = product.Quantity > orderquantity ? (orderquantity) : (product.Quantity);//alabilceğim ürün
                var purchased = customerWallet.Money >= canbuy * product.UnitPrice ? canbuy : (customerWallet.Money / product.UnitPrice);//verilen ürün
                orderquantity -= purchased;//istediğim kilodan verilen kilo çıkarılıyor

                if (purchased > 0) //verilen kilo 0 dan büyükse
                {
                    var supplierWallet = _userWalletService.GetById(product.SupplierId).Data; //satıcının cüzdanının verileri çekiliyor
                    customerWallet.Money -= purchased * product.UnitPrice; // verilen kilo ile ürünün fiyatı çarpılıp müşterinin cüzdanındaki paradan eksiliyor
                    supplierWallet.Money += purchased * product.UnitPrice; //o para satıcıya ekleniyor
                    accountantWallet.Data.Money += purchased * product.UnitPrice / 100; //o paranın 100 e bölümü muhasebeciye gidiyor
                    supplierWallets.Add(supplierWallet);//satıcı cüzdanına ekleme          
                    product.Quantity -= purchased;//ürünün kilosu verilen ürün kadaar eksiliyor
                    matchingProducts.Add(product); //eşleşen ürün ekleniyor
                    orderDetails.Add(new OrderDetail { ProductId = product.ProductId, Quantity = purchased, SupplierId = product.SupplierId, OrderDate=DateTime.Now});
                }

                if (orderquantity == 0)//istediğim kilo 0 olmuşsa
                {
                    
                    _userWalletService.Update(customerWallet);//müşteri cüzdanı güncellendi
                    _userWalletService.Update(accountantWallet.Data);//muhasebecinin cüzdanı güncellendi
                    _userWalletService.UpdateList(supplierWallets);
                    _productService.UpdateList(matchingProducts);
                    if (order.OrderId > 0) // order var demek
                    {
                        order.OrderPending = false;// bekleyen ürün false oldu yani yok
                        _orderDal.Update(order);//sipariş güncellenedi
                    }
                    else
                    {
                        order.OrderDate = DateTime.Now;//sipariş zamanı
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
            //bekleyen sipariş listesi
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(o => o.OrderPending == true && o.ProductName == productName.ProductName));
        }

        public IDataResult<List<Order>> GetByReport(ReportDetail reportDetail)
        { 
            //rapor oluşturma kısmı
            var result = _orderDal.GetAll(o => o.CustomerId == reportDetail.UserId && o.OrderDate > reportDetail.StartDate
             && o.OrderDate < reportDetail.EndDate && o.OrderPending == false);
            return new SuccessDataResult<List<Order>>(result);
        }
    }


}

