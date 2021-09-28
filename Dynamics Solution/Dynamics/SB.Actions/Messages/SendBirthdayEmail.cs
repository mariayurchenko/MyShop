using System;
using System.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.EntityProviders;
using SB.Shared.Extensions;
using SB.Shared.Models.Actions;
using SB.Shared.Models.Dynamics;

namespace SB.Actions.Messages
{
    class SendBirthdayEmail : IActionTracking
    {
        public IOrganizationService Service;
        public SendBirthdayEmail(IOrganizationService service)
        {
            this.Service = service;
        }
        public void Execute(string parameters, ref ActionResponse actionResponse)
        {
            var settings = new CustomSettings(Service).GetSettings(CustomSettingsModel.Fields.Usertosendemail);

            if (settings.Usertosendemail == null)
            {
                throw new Exception($"{nameof(settings.Usertosendemail)} is null");
            }

            var id = Guid.Parse(parameters);

            var contact = new Contact(Service, id);

            var templateTitle = "Birthday congratulation template";

            var query = new QueryExpression(EmailTemplateModel.LogicalName);
            query.Criteria.AddCondition(EmailTemplateModel.Fields.Title, ConditionOperator.Equal, templateTitle);
            var emailTemplate = Service.RetrieveMultiple(query).Entities.FirstOrDefault();

            if (emailTemplate == null)
            {
                throw new Exception("Template is doesn't exist");
            }

            var fromParty = new Entity(ActivityPartyModel.LogicalName);
            fromParty[ActivityPartyModel.Fields.Party] = new EntityReference(UserModel.LogicalName, settings.Usertosendemail.Id);

            var toParty = new Entity(ActivityPartyModel.LogicalName);
            toParty[ActivityPartyModel.Fields.Party] = new EntityReference(ContactModel.LogicalName, contact.Id.Value);

            var email = new Entity("email")
            {
                [EmailModel.Fields.To] = new[] { toParty },
                [EmailModel.Fields.From] = new[] { fromParty },
                [EmailModel.Fields.Direction] = true
            };

            var emailUsingTemplateReq = new SendEmailFromTemplateRequest
            {
                Target = email,
                TemplateId = emailTemplate.Id,
                RegardingId = contact.Id.Value,
                RegardingType = ContactModel.LogicalName
            };

            var emailUsingTemplateResp =
                (SendEmailFromTemplateResponse)Service.Execute(emailUsingTemplateReq);

            actionResponse = new ActionResponse { Value = emailUsingTemplateResp.Results.ToString() };
            actionResponse.Status = Status.Success;
        }
    }
}