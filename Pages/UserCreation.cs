using Assignments.CommonUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignments.Pages
{
    class UserCreation : Commonlib
    {
        private IWebDriver driver = null;
        Random randomGenerator = new Random();
        

        public UserCreation(IWebDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        IWebElement SignInButton => this.driver.FindElement(By.Id("nav-link-accountList"));

        IWebElement StartHere => this.driver.FindElement(By.LinkText("Start here."));

        IWebElement YourNameInputField => this.driver.FindElement(By.CssSelector("input[id='ap_customer_name']"));

        IWebElement YourEmailInputField => this.driver.FindElement(By.CssSelector("input[id='ap_email']"));

        IWebElement YourPasswordInputField => this.driver.FindElement(By.CssSelector("input[id='ap_password']"));

        IWebElement ReEnterYourPasswordInputField => this.driver.FindElement(By.CssSelector("input[id='ap_password_check']"));

        IWebElement CreateYourAmazonAccount => this.driver.FindElement(By.Id("continue"));

        IWebElement ErrorMessage => this.driver.FindElement(By.XPath("//div[contains(@class,'a-section auth-pagelet-container')]/*//h4[contains(@class,'a-alert-heading')]"));


        

        public void HoverToSignInButton()
        {
            this.ClickingTHeElementAfterMouseHovering(this.SignInButton);
        }

        public string GetErrorText()
        {
           return this.GetText(this.ErrorMessage);
        }

        public void EnterYouName(string value)
        {
            int r = randomGenerator.Next(10, 10099);
            this.EnterValue(this.YourNameInputField, value + r);
        }

        public void EnterYouEmail(string value)
        {
            this.EnterValue(this.YourEmailInputField, value);
        }

        public void EnterYouPassword(string value)
        {
            this.EnterValue(this.YourPasswordInputField, value);
        }

        public void ReEnterYouPassword(string value)
        {
            this.EnterValue(this.ReEnterYourPasswordInputField, value);
        }
        public void HoverToStartHereLink()
        {
            this.ClickElementAndWaitForPageLoad(this.StartHere);
        }

        public void CreateYourAmazonAccountButton()
        {
            this.ClickElementAndWaitForPageLoad(this.CreateYourAmazonAccount);
        }
    }
}
