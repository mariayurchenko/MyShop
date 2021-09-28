using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace Tests.Dynamics.Actions
{
    [TestClass]
    public class RetrieveBirthdayContactsTest : DynamicsTestBase<SB.Actions.ActionTracking>
    {
        [TestMethod]
        public void SuccessfullyGettingContactsWithTodayBirthday()
        {
            var executionContext = XrmRealContext.GetDefaultPluginContext();

            executionContext.MessageName = "sb_ActionTracking";
            executionContext.InputParameters = new ParameterCollection
            {
                new KeyValuePair<string, object>("ActionName", "RetrieveBirthdayContacts"),
                new KeyValuePair<string, object>("Parameters", "")
            };

            XrmRealContext.ExecutePluginWith<SB.Actions.ActionTracking>(executionContext);
        }
    }
}
