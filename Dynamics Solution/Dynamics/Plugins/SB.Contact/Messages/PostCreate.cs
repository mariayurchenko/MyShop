using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using SB.Shared.Models.Dynamics;

namespace SB.Contact.Messages
{
    public class PostCreate: IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            try
            {
                IOrganizationServiceFactory serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                var contact = (Entity)context.InputParameters["Target"];

                var initialize = new InitializeFromRequest();
                initialize.TargetEntityName = LoyaltyCardModel.LogicalName;
                initialize.EntityMoniker = contact.ToEntityReference();
                initialize.TargetFieldType = TargetFieldType.All;

                Entity loyaltyCard = ((InitializeFromResponse)service.Execute(initialize)).Entity;
                loyaltyCard[LoyaltyCardModel.Fields.Client] = contact.ToEntityReference();
                service.Create(loyaltyCard);

            }
            catch (InvalidPluginExecutionException e)
            {
                throw new InvalidPluginExecutionException("An error has occurred: " + e.Message);
            }
            catch (Exception ex)
            {
                tracingService.Trace("AutonumeratorPlugin: ", ex.ToString());
                throw;
            }
        }
    }
}