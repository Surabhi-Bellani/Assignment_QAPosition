using OpenQA.Selenium;
using System;
using System.Configuration;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using System.Resources;
using System.IO;

namespace Assignments.CommonUtility
{
    public class CommonUtils
    {
        private IWebDriver driver = null;
        string browser = ConfigurationManager.AppSettings["browser"];
        private static ResXResourceSet resource = new ResXResourceSet(GetFilePath("TestData", "TestResource.resx"));

        public CommonUtils(IWebDriver driver)
        {
            
            try
            { 
                this.driver = driver;
            }
            catch (Exception e)
            {
                 Assert.Fail("Page not Loadedmessage : {0} ", e.Message);
            }
        }

     public void WaitForPageLoad()
        {
            try
            {
                
                IWait<IWebDriver> wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(5000));
                wait.Until(driver => ((IJavaScriptExecutor)this.driver).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch (Exception e)
            {
                Assert.Fail("Page not Loadedmessage : {0} ", e.Message);
            }
        }

        public void WaitForElement(IWebElement element)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(5000));
                wait.Until(d => element.Displayed);
            }
            catch (Exception e)
            {
                Assert.Fail("Page not Loadedmessage : {0} ", e.Message);
            }
        }

        public void RefreshPage()
        {
            this.driver.Navigate().Refresh();
        }

        public void ClickElementAndWaitForPageLoad(IWebElement elementToClick)
        {
            this.ClickOnElement(elementToClick);
            this.WaitForPageLoad();
        }

        public void EnterValue(IWebElement element, string valueToBeEntered)
        {
            this.WaitForElement(element);
            element.Clear();

            if (browser == "IE")
            {
                this.EnterValueByJS(element, valueToBeEntered);
            }
            else
            {
                element.SendKeys(valueToBeEntered);
            }
        }

        public void EnterValueByJS(IWebElement element, string valueToBeEntered)
        {
            ((IJavaScriptExecutor)this.driver).ExecuteScript("arguments[0].value = arguments[1]", element, valueToBeEntered);
        }

        public void ScrollToViewTheElement(IWebElement element)
        {
            ((IJavaScriptExecutor)this.driver).ExecuteScript("arguments[0].scrollIntoView(true)", element);
        }

        public void ClickOnElement(IWebElement element)
        {
            this.WaitForElement(element);
            if (browser == "IE")
            {
                this.ClickUsingJS(element);
            }
            else
            {
                element.Click();
            }
        }

        public void ClickUsingJS(IWebElement element)
        {
            ((IJavaScriptExecutor)this.driver).ExecuteScript("arguments[0].click();", element);
        }

        public void ScrollUpThePage()
        {
            ((IJavaScriptExecutor)this.driver).ExecuteScript("window.scrollTo(document.body.scrollHeight,0)");
        }

        public string GetText(IWebElement element)
        {
            this.WaitForElement(element);
            this.WaitForPageLoad();
            var text = element.Text;
            return text;
        }

        public void ClickingTHeElementAfterMouseHovering(IWebElement element)
        {
            Actions actions = new Actions(this.driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public void SubmittingTheChoice(IWebElement element)
        {
            element.Submit();
        }

        public string SelectingTheDropdown()
        {
            SelectElement selectOption = new SelectElement(this.driver.FindElement(By.TagName("select")));
            return selectOption.SelectedOption.Text;
        }

        public static string GetComponentDirectoryPath(string packageName)
        {
            string binFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string actualPath = binFolder.Substring(0, binFolder.LastIndexOf("bin"));
            return Path.Combine(actualPath, @packageName);
        }

        public static string GetFilePath(string packageName, string fileName)
        {
            return Path.Combine(GetComponentDirectoryPath(packageName), fileName);
        }

        public static string GetResourceString(string key)
        {
                return resource.GetString(key);
        }
    }

}
