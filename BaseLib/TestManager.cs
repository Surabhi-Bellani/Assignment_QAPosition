using Assignment.Base;
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
