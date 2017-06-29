using FluentAssertions;
using System.Threading;
using Cibertec.Automation;
using Xunit;

namespace Cibertec.AutomationTest
{
    public class LoginPageTestNavigation
    {
        private readonly LoginPage _loginPage;
        public LoginPageTestNavigation()
        {
            Driver.GetInstance(Drivers.Chrome);
            _loginPage = new LoginPage();
        }

        [Theory(DisplayName ="Login")]
        [InlineData("juvega@gmail.com", "12345678")]
        public void LoginTest(string email, string password)
        {
            _loginPage.Go();
            _loginPage.LoginAs(email).WithPassword(password).Login();
            Thread.Sleep(1000);
            _loginPage.GetUrl().Should().Be("http://localhost/Cibertec.Angular/#!/product");
            _loginPage.Logout();
            Driver.CloseInstance();
        }

        [Fact(DisplayName = "Login Incorrect")]
        public void WrongLoginTest()
        {
            _loginPage.Go();
            _loginPage.LoginAs("juvega@test.net").WithPassword("incorrect").Login();
            Thread.Sleep(1000);
            _loginPage.IsAlertErrorPresent().Should().Be(true);
            Driver.CloseInstance();
        }        
    }
}
