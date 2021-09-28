using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace Tests.Dynamics.Actions
{
    [TestClass]
    public class CreateCheckTest : DynamicsTestBase<SB.Actions.ActionTracking>
    {
        [TestMethod]
        public void SuccessfulCreatingCheck()
        {
            var executionContext = XrmRealContext.GetDefaultPluginContext();

            executionContext.MessageName = "sb_ActionTracking";
            FileReader fileReader = new FileReader("Check1.txt");
            var body = fileReader.ReadFile();
            executionContext.InputParameters = new ParameterCollection
            {
                new KeyValuePair<string, object>("ActionName", "CreateCheck"),
                new KeyValuePair<string, object>("Parameters", body)
            };

            XrmRealContext.ExecutePluginWith<SB.Actions.ActionTracking>(executionContext);
        } 
        [TestMethod]
        [ExpectedException(typeof(InvalidPluginExecutionException))]
        public void WhenEnterInvalidDataThrowsException()
        {
            var executionContext = XrmRealContext.GetDefaultPluginContext();
            executionContext.MessageName = "sb_ActionTracking";
            FileReader fileReader = new FileReader("Check2.txt");
            var body = fileReader.ReadFile();
            executionContext.InputParameters = new ParameterCollection
            {
                new KeyValuePair<string, object>("ActionName", "CreateCheck"),
                new KeyValuePair<string, object>("Parameters", body)
            };

            XrmRealContext.ExecutePluginWith<SB.Actions.ActionTracking>(executionContext);
        }
        [TestMethod]
        public void WhenEmailNotFoundSendLoyaltyCardForm()
        {
            var executionContext = XrmRealContext.GetDefaultPluginContext();
     
            executionContext.MessageName = "sb_ActionTracking";
            FileReader fileReader = new FileReader("Check3.txt");
            var body = fileReader.ReadFile();
            executionContext.InputParameters = new ParameterCollection
            {
                new KeyValuePair<string, object>("ActionName", "CreateCheck"),
                new KeyValuePair<string, object>("Parameters", body)
            };
     
            XrmRealContext.ExecutePluginWith<SB.Actions.ActionTracking>(executionContext);
        }
    }
}