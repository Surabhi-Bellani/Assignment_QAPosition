using Assignment.Base;
using Assignments.CommonUtility;
using Assignments.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using TechTalk.SpecFlow;

namespace Assignments.BaseLib
{
    class PrePostConditions
    {
        [Binding]
        class PreConditionsManager
        {
            IWebDriver driver = DriverManager.GetDriver();
            Commonlib commonlib = new Commonlib(DriverManager.GetDriver());
        }
    }
}
