using FluentAssertions;
using System.Threading;
using Tibox.Automation;
using Xunit;

namespace Tibox.AutomationTest
{
    public class LoginPageTestNavigation
    {
        public LoginPageTestNavigation()
        {
            Driver.GetInstance(Drivers.Chrome);
        }

        [Theory(DisplayName ="Login")]
        [InlineData("juvega@gmail.com", "12345678")]
        public void LoginTest(string email, string password)
        {
            LoginPage.Go();
            LoginPage.LoginAs(email).WithPassword(password).Login();
            Thread.Sleep(1000);
            LoginPage.GetUrl().Should().Be("http://localhost/Cibertec.Angular/#!/product");
            LoginPage.Logout();
            Driver.CloseInstance();
        }

        [Fact(DisplayName = "Login Incorrect")]
        public void WrongLoginTest()
        {
            LoginPage.Go();
            LoginPage.LoginAs("juvega@test.net").WithPassword("incorrect").Login();
            Thread.Sleep(1000);
            LoginPage.IsAlertErrorPresent().Should().Be(true);
            Driver.CloseInstance();
        }        
    }
}
