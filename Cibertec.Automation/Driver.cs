using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;

namespace Tibox.Automation
{
    public enum Drivers{
        Chrome=0,
        InternetExplorer=1
    }

    public class Driver
    {
        public static IWebDriver Instance { get; set; }

        public static void GetInstance(Drivers option)
        {
            switch(option)
            {
                case Drivers.InternetExplorer:
                    Instance = IEInstance();
                    break;
                default:
                    Instance = ChromeInstance();
                    break;                
            }
            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        private static IWebDriver ChromeInstance()
        {
            var options = new ChromeOptions();
            options.AddArguments("chrome.switches", "--disable-extensions --disable-extensions-file-access-check --disable-extensions-http-throttling --disable-infobars --enable-automation --start-maximized");
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);         

            return new ChromeDriver(options);            
        }

        private static IWebDriver IEInstance()
        {
            return new InternetExplorerDriver();
        }

        public static void CloseInstance()
        {
            Instance.Close();
            Instance.Quit();
            Instance = null;
        }
    }
}