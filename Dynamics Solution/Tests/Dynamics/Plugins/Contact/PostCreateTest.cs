using System;
using System.Collections.Generic;
using SB.Contact.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace Tests.Dynamics.Plugins
{
    [TestClass]
    public class PostCreateTest : DynamicsTestBase<SB.Actions.ActionTracking>
    {
        [TestMethod]
        public void SuccessfulCreationContactFromLoyaltyCardForm()
        {
            var executionContext = XrmRealContext.GetDefaultPluginContext();

            Entity entity = new Entity("crdb1_loyaltycard") { 
                ["crdb1_email"]="MARjohn_snow@gmail.com",
                ["crdb1_phone"] = "123456789",
                ["crdb1_firstname"] = "MarJohn",
                ["crdb1_lastname"] = "MarSnow",
                ["crdb1_number"]="123456789"
            };

            executionContext.InputParameters = new ParameterCollection
            {
                new KeyValuePair<string, object>("Target", entity)
            };

            XrmRealContext.ExecutePluginWith<PostCreate>(executionContext);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void WhenCreatedWithInvalidDataThrowsException()
        {
            var executionContext = XrmRealContext.GetDefaultPluginContext();

            Entity entity = new Entity("crdb1_loyaltycard")
            {
                ["crdb1_email"] = ""
            };

            executionContext.InputParameters = new ParameterCollection
            {
                new KeyValuePair<string, object>("Target", entity)
            };

            XrmRealContext.ExecutePluginWith<PostCreate>(executionContext);
        }
    }
}