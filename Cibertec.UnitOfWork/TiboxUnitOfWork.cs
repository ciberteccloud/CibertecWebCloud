using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;
using Cibertec.Repository;
using Cibertec.Repository.Northwind;

namespace Cibertec.UnitOfWork
{
    public class TiboxUnitOfWork : IUnitOfWork
    {
        public TiboxUnitOfWork()
        {
            Customers = new CustomerRepository();
            Orders = new OrderRepository();
            OrderItems = new BaseRepository<OrderItem>();
            Products = new BaseRepository<Product>();
            Suppliers = new BaseRepository<Supplier>();
            Users = new UserRepository();         
        }
        public ICustomerRepository Customers { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IRepository<OrderItem> OrderItems { get; private set; }
        public IRepository<Product> Products { get; private set; }
        public IRepository<Supplier> Suppliers { get; private set; }
        public IUserRepository Users { get; private set; }
    }
}
