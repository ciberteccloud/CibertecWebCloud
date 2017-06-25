using Cibertec.Models;
using Cibertec.UnitOfWork;
using Moq;
using Ploeh.AutoFixture;
using System.Linq;

namespace Cibertec.Mocked
{
    public static class MockedUnitOfWork
    {
        public static IUnitOfWork GetUnitOfWork()
        {
            Mock<IUnitOfWork> unit = new Mock<IUnitOfWork>();
            unit.ConfigureCustomer();
            return unit.Object;
        }
    }

    public static class MockedUnitOfWorkExtensions
    {
        public static Mock<IUnitOfWork> ConfigureCustomer(this Mock<IUnitOfWork> mock)
        {
            var fixture = new Fixture();
            var customerId = 0;
            var customerList = fixture.CreateMany<Customer>(25).ToList();
            customerList.ForEach(x => 
            {
                x.Id = customerId++;
                x.FirstName = $"FirstName{customerId}";
                x.LastName = $"LastName{customerId}";
            });
            mock.Setup(c => c.Customers.GetEntityById(It.IsAny<int>())).Returns(
                (int id) => 
                {
                    return customerList.FirstOrDefault(x => x.Id == id);
                });

            mock.Setup(c => c.Customers.GetAll()).Returns(customerList);
            mock.Setup(c => c.Customers.Insert(It.IsAny<Customer>())).Returns(1);
            mock.Setup(c => c.Customers.Update(It.IsAny<Customer>())).Returns(true);
            mock.Setup(c => c.Customers.Delete(It.IsAny<Customer>())).Returns(true);
            mock.Setup(c => c.Customers.SearchByNames(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string firstName, string lastName) =>
                {
                    return customerList.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
                });
            return mock;
        }
    }
}