using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;

namespace Cibertec.Repository.Northwind
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer SearchByNames(string firstName, string lastName);
        Customer CustomerWithOrders(int id);
        IEnumerable<Customer> PagedList(int startRow, int endRow);
        int Count();
    }
}
