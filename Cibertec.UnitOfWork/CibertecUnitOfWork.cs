using Cibertec.Models;
using Cibertec.Repository;
using Cibertec.Repository.Northwind;

namespace Cibertec.UnitOfWork
{
    public class CibertecUnitOfWork : IUnitOfWork
    {
        public CibertecUnitOfWork()
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
