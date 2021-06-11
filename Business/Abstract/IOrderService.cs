using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IResult Add(Order order); //sipariş ekleme
        IDataResult<List<Order>> GetByProductNameOrderPending(Product productName); //ürün fiyatı uymamışsa bekleyen sipariş yapıyor
        IDataResult<List<Order>> GetByReport(ReportDetail reportDetail); //rapor
    }
}
