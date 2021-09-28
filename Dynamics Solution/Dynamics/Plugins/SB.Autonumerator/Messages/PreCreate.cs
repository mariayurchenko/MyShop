using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Extensions;
using SB.Shared.Models.Dynamics;

namespace SB.Autonumerator.Messages
{
    public class PreCreate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService) serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                var serviceFactory =
                    (IOrganizationServiceFactory) serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                var service = serviceFactory.CreateOrganizationService(context.UserId);

                if ((((Entity) context.InputParameters["Target"]).LogicalName.Equals(CheckModel.LogicalName)
                     || ((Entity) context.InputParameters["Target"]).LogicalName
                     .Equals(LoyaltyCardModel.LogicalName)))
                {
                    try
                    {
                        Entity entity = (Entity) context.InputParameters["Target"];

                        var query = new QueryExpression(AutonumeratorModel.LogicalName);
                        query.ColumnSet.AddColumns(AutonumeratorModel.Fields.Prefix,
                            AutonumeratorModel.Fields.EntityName, AutonumeratorModel.Fields.CurrentNumber);
                        query.Criteria.AddCondition(AutonumeratorModel.Fields.EntityName, ConditionOperator.Equal,
                            entity.LogicalName);

                        var autoNumerator = service.RetrieveMultiple(query)
                                                .ToEntityList<Shared.Extensions.Autonumerator>(service).FirstOrDefault()
                                            ?? throw new Exception("Autonumerator doesn't exist.");

                        entity["sb_number"] = $"{autoNumerator.Prefix}-{autoNumerator.CurrentNumber++}";

                        autoNumerator.Save();
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
    }
}