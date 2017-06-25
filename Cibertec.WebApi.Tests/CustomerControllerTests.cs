using Cibertec.Mocked;
using Cibertec.Models;
using Cibertec.WebApi.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Xunit;

namespace Cibertec.WebApi.Tests
{
    public class CustomerControllerTests
    {
        private readonly CustomerController _controller;
        public CustomerControllerTests()
        {
            _controller = new CustomerController(MockedUnitOfWork.GetUnitOfWork());
        }
                
        [Fact]
        public void Customer_Get_By_Id()
        {
            var result = _controller.Get(1) as OkNegotiatedContentResult<Customer>;
            Assert.NotNull(result);
            Assert.True(result.Content.Id == 1);
        }

        [Fact]
        public void Customer_Get_All()
        {
            var result = _controller.GetList() as OkNegotiatedContentResult<IEnumerable<Customer>>;
            Assert.NotNull(result);
            Assert.True(result.Content.Count()==25);
        }
    }
}
