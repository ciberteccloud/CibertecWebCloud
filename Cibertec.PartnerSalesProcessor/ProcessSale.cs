using Cibertec.Models;
using Cibertec.UnitOfWork;
using ClosedXML.Excel;
using System;

namespace Cibertec.PartnerSalesProcessor
{
    public class ProcessSale
    {
        private readonly IUnitOfWork _unit;
        public ProcessSale(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public void ReadExcel(string fileName)
        {            
            var workbook = new XLWorkbook(fileName);
            workbook.CalculateMode = XLCalculateMode.Auto;

            var sale = new Sale();

            if (workbook.Worksheet(1).Name == "Customer")
            {
                var row = workbook.Worksheet(1).Row(2);
                sale.Customer = new Customer
                {
                    FirstName = row.Cell("B").GetString(),
                    LastName = row.Cell("C").GetString(),
                    City = row.Cell("D").GetString(),
                    Country = row.Cell("E").GetString(),
                    Phone = row.Cell("F").GetString()
                };                
            }

            if (workbook.Worksheet(2).Name == "Order")
            {
                int index = 2;
                while (!workbook.Worksheet(2).Row(index).IsEmpty())
                {
                    var row = workbook.Worksheet(2).Row(index);
                    sale.Orders.Add(new Order
                    {
                        CustomerId = sale.Customer.Id,
                        OrderDate = row.Cell("B").GetDateTime(),
                        OrderNumber = row.Cell("C").GetString(),
                        TotalAmount = Convert.ToDecimal(row.Cell("E").ValueCached)
                    });
                    index++;
                }
            }

            if (workbook.Worksheet(3).Name == "OrderItem")
            {
                int index = 2;
                while (!workbook.Worksheet(3).Row(index).IsEmpty())
                {
                    var row = workbook.Worksheet(3).Row(index);
                    sale.OrderItems.Add(new OrderItem
                    {
                        OrderId = row.Cell("B").GetValue<int>(),
                        ProductId= row.Cell("C").GetValue<int>(),
                        UnitPrice= Convert.ToDecimal(row.Cell("D").ValueCached),
                        Quantity= row.Cell("E").GetValue<int>()
                    });
                    index++;
                }
            }

            _unit.Sales.ProcessSale(sale);
            
        }
    }
}
