using Assignment.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
namespace Assignments.BaseLib
{
    [Binding]
    class TestManager
    {
        [BeforeScenario]
        public static void PreSetup()
        {
            // The PreSetup to InitializeBrowser.
            DriverManager.InitializeBrowser();
        }


        [AfterScenario]
        public static void Teardown()
        {
            DriverManager.CloseBrowser();
        }
    }
}
