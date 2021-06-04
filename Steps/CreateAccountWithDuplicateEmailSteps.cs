using Assignment.Base;
using Assignments.CommonUtility;
using Assignments.Pages;
using IronOcr;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace Assignment.Steps
{
    [Binding]
    public class CreateAccountSteps
    {
        IWebDriver driver = null;
        private UserCreation userCreation;
        public CreateAccountSteps()
        {
            this.driver = DriverManager.GetDriver();
            userCreation = new UserCreation(driver);
        }

        [Given(@"User hover SignIn option")]
        public void GivenUserHoverSignInOption()
        {
            userCreation.HoverToSignInButton();
        }

        [Given(@"Click on Start here")]
        public void GivenClickOnStartHere()
        {
            userCreation.HoverToStartHereLink();
        }

        [Given(@"User is navigated to User Creation Page")]
        public void GivenUserIsNavigatedToUserCreationPage()
        {
            userCreation.WaitForPageLoad();
        }

        [When(@"User enters valid '(.*)','(.*)','(.*)','(.*)'")]
        public void WhenUserEntersValid(string p0, string p1, string p2, string p3)
        {
            userCreation.EnterYouName(Commonlib.GetResourceString(p0));
            userCreation.EnterYouEmail(Commonlib.GetResourceString(p1));
            userCreation.EnterYouPassword(Commonlib.GetResourceString(p2));
            userCreation.ReEnterYouPassword(Commonlib.GetResourceString(p3));
        }

        [When(@"User Click on Create your Amazon Account")]
        public void WhenUserClickOnCreateYourAmazonAccount()
        {
            userCreation.CreateYourAmazonAccountButton();
        }

        [Then(@"error message is displayed")]
        public void ThenErrorMessageIsDisplayed()
        {
            string Error = userCreation.GetErrorText();
            Assert.AreEqual("Email address already in use", Error);
        }

    }
}
