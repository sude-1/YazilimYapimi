using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IResult Add(Order order);
        IDataResult<List<Order>> GetByProductNameOrderPending(Product productName);
        IDataResult<List<Order>> GetByReport(ReportDetail reportDetail);
    }
}
