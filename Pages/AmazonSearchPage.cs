namespace Assignments.Pages
{
    using Assignments.CommonUtility;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    public class AmazonSearchPage : CommonUtils
    {
        private IWebDriver driver = null;


        public AmazonSearchPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        IWebElement Title => this.driver.FindElement(By.XPath("//head/title"));

        IWebElement CategoryDropdown => this.driver.FindElement(By.Id("searchDropdownBox"));

        IWebElement SearchTextBox => this.driver.FindElement(By.Id("twotabsearchtextbox"));

        IWebElement SearchButton => this.driver.FindElement(By.Id("nav-search-submit-button"));

        IList<IWebElement> ListOfResults => this.driver.FindElements(By.XPath("//h2/a/span[contains(@class,'a-size-medium')]"));

        IWebElement Result => this.driver.FindElement(By.XPath("//h2/a/span[contains(@class,'a-size-medium')]"));

        IList<string> ListOfOptionsInString => this.ListOfResults.Select(c => c.Text).ToList();

        IWebElement AddToCartButton => this.driver.FindElement(By.Id("add-to-cart-button"));

        IWebElement SelectLocation => this.driver.FindElement(By.Id("glow-ingress-block"));

        IWebElement InputZipCode => this.driver.FindElement(By.Id("GLUXZipUpdateInput"));

        IWebElement ApplyPinCode => this.driver.FindElement(By.CssSelector("div[role='button']"));

        IWebElement ClickDone => this.driver.FindElement(By.Name("glowDoneButton"));

        IWebElement CartIcon => this.driver.FindElement(By.Id("nav-cart-count-container"));

        IWebElement ItemTotal => this.driver.FindElement(By.Id("sc-subtotal-label-buybox"));

        IWebElement FilteringByPrice => this.driver.FindElement(By.Id("p_36/14674873011"));

        public void SelectingLocation()
        {
            this.ClickElementAndWaitForPageLoad(this.SelectLocation);
            this.driver.SwitchTo().ActiveElement();
            this.EnterValue(this.InputZipCode, "20505");
            this.ClickOnElement(this.ApplyPinCode);
            this.ClickOnElement(this.ClickDone);
            Thread.Sleep(3000);
            this.driver.SwitchTo().ActiveElement();
        }

        public string GetTheNumberOfItemInCart()
        {
            this.ClickOnElement(this.CartIcon);
            return this.GetText(this.ItemTotal);
        }

        public string GetTheTitle()
        {
            return this.GetText(this.Title);
        }

        public string SelectingAllCategoriesOption()
        {
            return this.SelectingTheDropdown();
        }

        public void SubmittingTheSearchItem()
        {
            this.SubmittingTheChoice(this.SearchButton);
            this.ClickOnElement(this.SearchButton);
        }

        public void AddingTheItemToTheCart()
        {
            this.SubmittingTheChoice(this.AddToCartButton);
        }

        public string CreateFolder()
        {
            string currentFolder = CommonUtils.GetComponentDirectoryPath("ItemsList") + Path.AltDirectorySeparatorChar;
            if (!Directory.Exists(currentFolder))
            {
                Directory.CreateDirectory(currentFolder);
            }

            return currentFolder;
        }

        public void ClickingThePriceFilter()
        {
            this.ScrollToViewTheElement(this.FilteringByPrice);
            this.ClickElementAndWaitForPageLoad(this.FilteringByPrice);
        }


        public void EnteringTheItemNameInSearchTextBox(string itemName)
        {
            this.ClickElementAndWaitForPageLoad(this.SearchTextBox);
            this.EnterValue(this.SearchTextBox, itemName);
        }

        public void HoveringAndCLickingTheMostMatchingItem(string itemName)
        {

            try
            {
                IList<string> optionsDisplayed = new List<string>();
                int count = 0;
                foreach (var element in this.ListOfOptionsInString)
                {
                    
                    var itemNameInArray = itemName.ToLookup(x => x);
                    var elementNameInArray = element.ToLookup(x => x);

                    if (itemNameInArray.All(xs => xs.Count() <= elementNameInArray[xs.Key].Count()))
                    {
                        this.ClickElementAndWaitForPageLoad(this.ListOfResults[count]);
                        this.WaitForPageLoad();
                        break;
                    }
                    else
                    {
                        count++;
                        continue;
                    }
                }
            }
            catch (Exception e)
            {
                this.ClickElementAndWaitForPageLoad(this.ListOfResults[1]);
                Console.WriteLine("No Matching Results found : {0}", e.Message);
            }
        }

        public void SelectingThePriceFilter()
        {
            this.ClickingThePriceFilter();
        }


        public void AddingItemListInTextFile()
        {
            try
            {

                IList<string> optionsDisplayed = new List<string>();
                IList<string> newStringList = new List<string>();
                do
                {
                    optionsDisplayed = this.ListOfOptionsInString;

                    foreach (var element in optionsDisplayed)
                    {
                        newStringList.Add(element + System.Environment.NewLine);
                        System.IO.File.AppendAllText(this.CreateFolder() + Path.AltDirectorySeparatorChar + "Items.txt", element + System.Environment.NewLine);
                    }
                }
                while (optionsDisplayed.Count == 0);
            }
            catch (Exception e)
            {
                this.ClickElementAndWaitForPageLoad(this.ListOfResults[1]);
                Console.WriteLine("No Matching Results found : {0}", e.Message);
            }
        }


        public void AddingTheItemToCart()
        {
            try
            {
                this.WaitForElement(this.AddToCartButton);
                this.SubmittingTheChoice(this.AddToCartButton);
            }
            catch (Exception e)
            {
                Console.WriteLine("ItemNotAvailableForDelivery : {0}", e.Message);
            }
        }

    }
}
