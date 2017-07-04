using System.Collections.Generic;

namespace Cibertec.Models
{
    public class Sale
    {
        public Sale()
        {
            Orders = new List<Order>();
            OrderItems = new List<OrderItem>();
        }
        public Customer Customer { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
