using System.Linq;
using Cibertec.Models;
using System.Configuration;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace Cibertec.Repository.Northwind
{
    public class SaleRepository : ISaleRepository
    {
        protected readonly string _connectionString;
        public SaleRepository()
        {
            _connectionString = ConfigurationManager
                    .ConnectionStrings["NorthwindConnectionString"]
                    .ConnectionString;
        }
        public bool ProcessSale(Sale sale)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var customerId = (int)connection.Insert(sale.Customer, transaction);


                        for (int i = 0; i < sale.Orders.Count; i++)
                        {
                            sale.Orders[i].CustomerId = customerId;
                            var orderId = (int)connection.Insert(sale.Orders[i], transaction);
                            sale.OrderItems.Where(item => item.OrderId == i+1).ToList().ForEach(order => order.OrderId = orderId);
                        }

                        foreach (var order in sale.OrderItems)
                        {
                            connection.Insert(order, transaction);
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return true;
        }
    }
}
