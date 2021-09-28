using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using SB.Actions.Messages;
using SB.Shared;
using SB.Shared.Models.Actions;

namespace SB.Actions
{
    public class ActionTracking : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.Get<IPluginExecutionContext>();
            var factory = serviceProvider.Get<IOrganizationServiceFactory>();
            var service = factory.CreateOrganizationService(context.UserId);
            var tracer = serviceProvider.Get<ITracingService>();
            var notificationService = serviceProvider.Get<IServiceEndpointNotificationService>();
            try
            {

                var actionName = (string)context.InputParameters["ActionName"];
                var parameters = context.InputParameters.Contains("Parameters") ? (string)context.InputParameters["Parameters"] : string.Empty;

                var response = new ActionResponse();

                foreach (var parameter in context.InputParameters)
                {
                    tracer?.Trace($"{parameter.Key} = {parameter.Value}");
                }

                IActionTracking handler;
                switch (actionName)
                {
                    case ActionNames.ActionTrackingNames.CreateCheck:
                        handler = new CreateCheck(service);
                        break;
                    case ActionNames.ActionTrackingNames.SendLoyaltyCardForm:
                        handler = new SendLoyaltyCardForm(service);
                        break;
                    case ActionNames.ActionTrackingNames.SendBirthdayEmail:
                        handler = new SendBirthdayEmail(service);
                        break;
                     case ActionNames.ActionTrackingNames.RetrieveBirthdayContacts:
                        handler = new RetrieveBirthdayContacts(service, tracer);
                        break;

                    default:
                        return;
                }

                handler.Execute(parameters, ref response);

                context.OutputParameters["Response"] = JsonSerializer.Serialize(response);

                tracer?.Trace($"{nameof(context.OutputParameters)}:");
                foreach (var parameter in context.OutputParameters)
                {
                    tracer?.Trace($"{parameter.Key} = {parameter.Value}");
                }
            }
            catch (Exception e)
            {
                    throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}