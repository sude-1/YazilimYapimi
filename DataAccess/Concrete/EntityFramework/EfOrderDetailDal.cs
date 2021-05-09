using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDetailDal : EfEntityRepositoryBase<OrderDetail, YazilimYapimiContext>, IOrderDetailDal
    {
        public List<OrderDetailDto> GetOrderDetails()
        {
            using (YazilimYapimiContext context = new YazilimYapimiContext())
            {
                var result = from orderDetail in context.OrderDetails
                             join product in context.Products
                             on orderDetail.ProductId equals product.ProductId
                             join order in context.Orders
                             on orderDetail.OrderId equals order.OrderId
                             join user in context.Users
                             on orderDetail.SupplierId equals user.Id
                             join category in context.Categories
                             on product.CategoryId equals category.CategoryId
                             select new OrderDetailDto
                             {
                                 Id = orderDetail.Id,
                                 ProductName = product.ProductName,
                                 UserName=user.UserName,
                                 OrderDate=order.OrderDate,
                                 Quantity=product.Quantity,
                                 Total=product.UnitPrice * orderDetail.Quantity
                             };
                return result.ToList();
            }
        }
    }
}
