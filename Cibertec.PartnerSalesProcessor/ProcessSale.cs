using Cibertec.Models;
using Cibertec.UnitOfWork;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;

namespace Cibertec.PartnerSalesProcessor
{
    public class ProcessSale
    {
        private readonly IUnitOfWork _unit;
        public ProcessSale(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public void ReadExcel()
        {
            string fileName = "PartnerSale.xlsx";
            var workbook = new XLWorkbook(fileName);
            workbook.CalculateMode = XLCalculateMode.Auto;

            Customer customer = new Customer();
            List<Order> orders = new List<Order>();
            List<OrderItem> orderItems = new List<OrderItem>();

            if (workbook.Worksheet(1).Name == "Customer")
            {
                var row = workbook.Worksheet(1).Row(2);
                customer = new Customer
                {
                    FirstName = row.Cell("B").GetString(),
                    LastName = row.Cell("C").GetString(),
                    City = row.Cell("D").GetString(),
                    Country = row.Cell("E").GetString(),
                    Phone = row.Cell("F").GetString()
                };
                //_unit.Customers.Insert(customer);
            }

            if (workbook.Worksheet(2).Name == "Order")
            {
                int index = 2;
                while (!workbook.Worksheet(2).Row(index).IsEmpty())
                {
                    var row = workbook.Worksheet(2).Row(index);
                    orders.Add(new Order
                    {
                        CustomerId = customer.Id,
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
                    orderItems.Add(new OrderItem
                    {
                        OrderId = row.Cell("B").GetValue<int>(),
                        ProductId= row.Cell("C").GetValue<int>(),
                        UnitPrice= Convert.ToDecimal(row.Cell("D").ValueCached),
                        Quantity= row.Cell("E").GetValue<int>()
                    });
                    index++;
                }
            }
            
        }
    }
}
