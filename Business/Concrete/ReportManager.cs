using Business.Abstract;
using ChoETL;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class ReportManager : IReportService
    {
        // çok düznelme annecim
        IOrderService _orderService;
        IProductService _productService;
        public ReportManager(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        public IDataResult<List<Report>> ReportDetay(ReportDetail reportDetail)
        {
            var result = _orderService.GetByReport(reportDetail).Data;
            var report = new List<Report>();
            foreach (var order in result)
            {
                report.Add(new Report { Sum = order.UnitPrice, Quantity = order.Quantity, Date = order.OrderDate, Product = order.ProductName });
            }
            return new SuccessDataResult<List<Report>>(report);
        }

        public IResult Reporting(ReportDetail reportDetail)
        {
            var result = ReportDetay(reportDetail);
           
            try
            {
                File.Delete($"wwwroot/csv/{reportDetail.UserId}.csv");
                using (var w = new ChoCSVWriter<Report>($"wwwroot/csv/{reportDetail.UserId}.csv").WithFirstLineHeader())
                {
                    w.Write(result.Data);
                    w.Dispose();
                }

                return new SuccessResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new ErrorResult();
        }
    }
}

