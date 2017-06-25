using System.Collections.Generic;
using Cibertec.Models;

namespace Cibertec.Repository.Northwind
{
    public interface IOrderRepository: IRepository<Order>
    {
        Order OrderWithOrderItems(int id);
        Order OrderByOrderNumber(string orderNumber);
        bool SaveOrderAndOrderItems(Order order, IEnumerable<OrderItem> items);
    }
}
