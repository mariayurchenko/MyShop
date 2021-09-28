using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using SB.Shared.Models.Dynamics;
using SB.Shared.Models.Actions;
using SB.Shared.Extensions;

namespace SB.Actions.Messages
{
    public class SendLoyaltyCardForm : IActionTracking
    {
        public IOrganizationService Service;
        public SendLoyaltyCardForm(IOrganizationService service) {
            this.Service = service;
        }
        public void Execute(string parameters, ref ActionResponse actionResponse)
        {
            var settings = new CustomSettings(Service).GetSettings(CustomSettingsModel.Fields.Usertosendemail);

            if(settings.Usertosendemail == null)
            {
                throw new Exception($"{nameof(settings.Usertosendemail)} is null");
            }

            var userId = settings.Usertosendemail.Id;

            string templateTitle = "Offer to issue a card";

            var query = new QueryExpression(EmailTemplateModel.LogicalName);
            query.Criteria.AddCondition(EmailTemplateModel.Fields.Title, ConditionOperator.Equal, templateTitle);
            var emailTemplate = Service.RetrieveMultiple(query).Entities.FirstOrDefault();

            if (emailTemplate == null)
            {
                throw new Exception("Template is doesn't exist");
            }
    
            var generatedCheck = Service.Retrieve(CheckModel.LogicalName, new Guid(parameters),
                new ColumnSet(CheckModel.Fields.Number, CheckModel.Fields.EmailAddress));
            var emailCheck = generatedCheck.GetAttributeValue<string>(CheckModel.Fields.EmailAddress);

            var fromParty = new Entity(ActivityPartyModel.LogicalName);
            fromParty[ActivityPartyModel.Fields.Party] = new EntityReference(UserModel.LogicalName, userId);

            var toParty = new Entity(ActivityPartyModel.LogicalName);
            toParty[ActivityPartyModel.Fields.Party] = new EntityReference(CheckModel.LogicalName, generatedCheck.Id);

            var email = new Entity("email")
            {
                [EmailModel.Fields.To] = new [] { toParty },
                [EmailModel.Fields.From] = new[] { fromParty },
                [EmailModel.Fields.Direction] = true
            };

            SendEmailFromTemplateRequest emailUsingTemplateReq = new SendEmailFromTemplateRequest
            {
                Target = email,
                TemplateId = emailTemplate.Id,
                RegardingId = userId,
                RegardingType = UserModel.LogicalName
            };

            SendEmailFromTemplateResponse emailUsingTemplateResp = (SendEmailFromTemplateResponse)Service.Execute(emailUsingTemplateReq);

            actionResponse = new ActionResponse { Value = emailUsingTemplateResp.ToString() };
        }
    }
}