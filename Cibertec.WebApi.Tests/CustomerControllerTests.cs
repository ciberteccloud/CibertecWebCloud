using Cibertec.UnitOfWork;
using Cibertec.WebApi.Controllers;
using System;
using System.Linq;
using Xunit;

namespace Cibertec.WebApi.Tests
{
    public class CustomerControllerTests
    {
        private readonly CustomerController _controller;
        public CustomerControllerTests()
        {
            _controller = new CustomerController(new CibertecUnitOfWork());
        }
                
        [Fact]
        public void Customer_Get_By_Id()
        {
            var result = _controller.Get(1);
            Assert.NotNull(result);
        }
    }
}
