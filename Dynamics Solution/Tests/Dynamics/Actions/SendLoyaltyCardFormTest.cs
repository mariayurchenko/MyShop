using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace Tests.Dynamics.Actions
{
    [TestClass]
    public class SendLoyaltyCardFormTest : DynamicsTestBase<SB.Actions.ActionTracking>
    {
        [TestMethod]
        public void SendEmail()
        {
            var executionContext = XrmRealContext.GetDefaultPluginContext();

            executionContext.MessageName = "sb_ActionTracking";

            var body = "pd12.1211@gmail.com";

            executionContext.InputParameters = new ParameterCollection
            {
                new KeyValuePair<string, object>("ActionName", "SendLoyaltyCardForm"),
                new KeyValuePair<string, object>("Parameters", body)
            };

            XrmRealContext.ExecutePluginWith<SB.Actions.ActionTracking>(executionContext);
        }
    }
}