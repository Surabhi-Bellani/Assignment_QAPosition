using Assignment.Base;
using Assignments.CommonUtility;
using Assignments.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Assignment.Steps
{
    [Binding]
    public class CreateAccountSteps
    {
        IWebDriver driver = null;
        private UserCreationPage userCreation;
        public CreateAccountSteps()
        {
            this.driver = DriverManager.GetDriver();
            userCreation = new UserCreationPage(driver);
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
            userCreation.EnterYouName(CommonUtils.GetResourceString(p0));
            userCreation.EnterYouEmail(CommonUtils.GetResourceString(p1));
            userCreation.EnterYouPassword(CommonUtils.GetResourceString(p2));
            userCreation.ReEnterYouPassword(CommonUtils.GetResourceString(p3));
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
