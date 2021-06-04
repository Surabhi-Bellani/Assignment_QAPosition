using Assignment.Base;
using Assignments.CommonUtility;
using Assignments.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace Assignments.Features
{
    [Binding]
    public class AddingSelectItemInCartSteps
    {
        IWebDriver driver = null;
        private AmazonSearchPage amazonSearchPage;
        public AddingSelectItemInCartSteps()
        {
            this.driver = DriverManager.GetDriver();
            amazonSearchPage = new AmazonSearchPage(driver);
        }

        [Given(@"Location is selected as US")]
        public void GivenLocationIsSelectedAsUS()
        {
            amazonSearchPage.SelectingLocation();
        }

        [Given(@"User is on Amazon home page")]
        public void GivenUserIsOnAmazonHomePage()
        {
            Assert.AreEqual(this.driver.Title, "Amazon.com: Online Shopping for Electronics, Apparel, Computers, Books, DVDs & more");
        }
        
        [Given(@"'(.*)' option is selected from the dropdown")]
        public void GivenOptionIsSelectedFromTheDropdown(string p0)
        {
            string SelectedValue = amazonSearchPage.SelectingAllCategoriesOption();
            Assert.AreEqual(p0, SelectedValue);
        }
        
        [Given(@"User Enters (.*) in the Search box")]
        public void GivenUserEntersInTheSearchBox(string ItemName)
        {
            amazonSearchPage.EnteringTheItemNameInSearchTextBox(Commonlib.GetResourceString(ItemName));
        }
        
        [When(@"User clicks on Search")]
        public void WhenUserClicksOnSearch()
        {
            amazonSearchPage.SubmittingTheSearchItem();
            Thread.Sleep(5000);
        }
        
        [When(@"User clicks on the product link")]
        public void WhenUserClicksOnTheProductLink()
        {
            amazonSearchPage.HoveringAndCLickingTheMostMatchingItem(Commonlib.GetResourceString("ItemName"));
        }
        [Then(@"User Clicks on '(.*)'")]
        public void ThenUserClicksOn(string p0)
        {
            amazonSearchPage.AddingTheItemToCart();
        }

        [Then(@"Item is moved to the Cart")]
        public void ThenItemIsMovedToTheCart()
        {
            string itemsFromUI = amazonSearchPage.GetTheNumberOfItemInCart();
            Assert.AreEqual(itemsFromUI, "Subtotal (1 item):");
        }

        [When(@"Select the Price Filter")]
        public void WhenSelectThePriceFilter()
        {
            amazonSearchPage.SelectingThePriceFilter();
        }

        [Then(@"User sees all the options presented for that item")]
        public void ThenUserSeesAllTheOptionsPresentedForThatItem()
        {
            amazonSearchPage.ScrollUpThePage();
        }


        [Then(@"User Wants to Save all the data in Txt file")]
        public void ThenUserWantsToSaveAllTheDataInTxtFile()
        {
            amazonSearchPage.AddingItemListInTextFile();
        }

    }
}
