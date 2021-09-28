using System.Collections.Generic;
using SB.Autonumerator.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace Tests.Dynamics.Plugins
{
    [TestClass]
    public class PreCreateTest : DynamicsTestBase<SB.Actions.ActionTracking>
    {
        [TestMethod]
        [DataRow("crdb1_check")]
        [DataRow("crdb1_loyaltycard")]
        public void SuccessfulEntityAutonumbering(string entityName)
        {
            var executionContext = XrmRealContext.GetDefaultPluginContext();

            Entity entity = new Entity(entityName);

            executionContext.InputParameters = new ParameterCollection
            {
                new KeyValuePair<string, object>("Target", entity)
            };

            XrmRealContext.ExecutePluginWith<PreCreate>(executionContext);
        }    
    }
}