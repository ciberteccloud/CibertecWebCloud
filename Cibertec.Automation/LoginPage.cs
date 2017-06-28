using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace Tibox.Automation
{
    public class LoginPage
    {
        const string url = "http://localhost/Cibertec.Angular";       

        #region Page Objects
        [FindsBy(How = How.CssSelector, Using = "a[href='#!/login']")]
        private IWebElement loginLink = null;

        [FindsBy(How = How.CssSelector, Using = "div.alert.alert-danger")]
        private IWebElement errorMessage = null;

        [FindsBy(How = How.CssSelector, Using = "a[ng-click='vm.logout()']")]
        private IWebElement logoutLink = null;
        #endregion 

        public LoginPage()
        {            
            PageFactory.InitElements(Driver.Instance, this);
        }
        public void Go()
        {
            Driver.Instance.Navigate().GoToUrl(url);
            loginLink.Click();
        }

        public LoginCommand LoginAs(string userName)
        {
            return new LoginCommand(userName);
        }

        public string GetUrl()
        {
            return Driver.Instance.Url;
        }

        public bool IsAlertErrorPresent()
        {
            return errorMessage==null;            
        }

        public void Logout()
        {   
            logoutLink.Click();
        }        
    }

    public class LoginCommand
    {
        #region Page Objects
        [FindsBy(How = How.Id, Using = "userName")]
        private IWebElement userNameInput = null;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordInput = null;

        [FindsBy(How = How.CssSelector, Using = "button.btn.btn-primary")]
        private IWebElement submitButton = null;
        #endregion 

        private string userName;
        private string password;

        public LoginCommand(string userName)
        {
            this.userName = userName;
            PageFactory.InitElements(Driver.Instance, this);
        }

        public LoginCommand WithPassword(string password)
        {
            this.password = password;
            return this;
        }

        public void Login()
        {
            userNameInput.Clear();
            userNameInput.SendKeys(userName);
            passwordInput.Clear();
            passwordInput.SendKeys(password);
            Submit();
        }

        public void Submit()
        {            
            submitButton.Click();
        }
    }
}
