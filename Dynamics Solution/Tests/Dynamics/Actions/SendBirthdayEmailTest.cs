using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace Tests.Dynamics.Actions
{
    [TestClass]
    public class SendBirthdayEmailTest : DynamicsTestBase<SB.Actions.ActionTracking>
    {

        [TestMethod]
        public void SuccessfulSendBirthdayEmail()
        {
            var executionContext = XrmRealContext.GetDefaultPluginContext();

            executionContext.MessageName = "sb_ActionTracking";
            var body = "8b28032a-e4c1-eb11-bacc-000d3abf0037";
            executionContext.InputParameters = new ParameterCollection
            {
                new KeyValuePair<string, object>("ActionName", "SendBirthdayEmail"),
                new KeyValuePair<string, object>("Parameters", body)
            };

            XrmRealContext.ExecutePluginWith<SB.Actions.ActionTracking>(executionContext);
        }
    }
}