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
        //EfOrderDetailDal implemente oluyor IOrderDetailDal dan ve oradai fonksiyonu kullanıyor
        public List<OrderDetailDto> GetOrderDetails()
        {
            using (YazilimYapimiContext context = new YazilimYapimiContext())
            {
                var result = from orderDetail in context.OrderDetails // orderDetail diye değişken oluşturup OrderDettails tablomla bağlıyorum
                             join product in context.Products // product diye değişken oluşturup Products tablomla bağlıyorum
                             on orderDetail.ProductId equals product.ProductId // ürünıd lerimi eşitliyorum
                             join order in context.Orders // order diye değişken oluşturup Orders tablomla bağlıyorum
                             on orderDetail.OrderId equals order.OrderId //orderId lerimi eşitliyorum
                             join user in context.Users // user diye değişken oluşturup Users tablomla bağlıyorum
                             on orderDetail.SupplierId equals user.Id //Satıcı id lerimi eşitliyorum
                             join category in context.Categories // category diye değişken oluşturup kategori tablomla bağlıyorum
                             on product.CategoryId equals category.CategoryId
                             select new OrderDetailDto
                             {
                                 //OrderDetailDto da yazdığım özelliklerimi needen alacağımı belirtiyorum
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
