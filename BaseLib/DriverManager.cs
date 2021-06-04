using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Assignment.Base
{
    class DriverManager
    {
        public static IWebDriver driver;
        public static string appURL = ConfigurationManager.AppSettings["URL"];
        public static string browser = ConfigurationManager.AppSettings["browser"];

        public static IWebDriver InitializeBrowser()
        {

            string binFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var service = ChromeDriverService.CreateDefaultService(binFolder);

            if (browser == "IE")
            {
                var options = new OpenQA.Selenium.IE.InternetExplorerOptions();
                options.IgnoreZoomLevel = true;
                options.EnsureCleanSession = true;
                options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                driver = new InternetExplorerDriver(options);
            }
            else if (browser == "Chrome")
            {
                ChromeOptions chromeCapabilities = new ChromeOptions();
                chromeCapabilities.AddArgument("start-maximized");
                chromeCapabilities.AddArguments("test-type");
                chromeCapabilities.AddArguments("disable-popup-blocking");
                chromeCapabilities.AddArguments("disable-default-apps");
                driver = new ChromeDriver(service, chromeCapabilities);
            }
           
            try
            {
                bool hasKeys = ConfigurationManager.AppSettings.HasKeys();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(90);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                driver.Navigate().GoToUrl(appURL);
            }
            catch
            {
                driver.Navigate().GoToUrl(appURL);
                Console.WriteLine(ConfigurationManager.AppSettings.Get("URL"));
            }

            return driver;
        }

        public static IWebDriver GetDriver()
        {
            return driver;
        }

        public static void CloseBrowser()
        {
            driver.Quit();
        }
    }
}

