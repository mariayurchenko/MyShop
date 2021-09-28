using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Actions;
using SB.Shared.Models.Dynamics;
using SB.Actions.Senders.QueueMessageSender;
using System;
using System.Collections.Generic;
using SB.Shared.Extensions;

namespace SB.Actions.Messages
{
    public class RetrieveBirthdayContacts : IActionTracking
    {
        public IOrganizationService Service;
        private readonly ITracingService _tracer;

        public RetrieveBirthdayContacts(IOrganizationService service, ITracingService tracer)
        {
            Service = service;
            _tracer = tracer;
        }

        public void Execute(string parameters, ref ActionResponse actionResponse)
        {
            var settings = new CustomSettings(Service).GetSettings(CustomSettingsModel.Fields.ServiceBusConnectionString, CustomSettingsModel.Fields.SendRequestQueueName);

            if (settings.ServiceBusConnectionString == null)
            {
                throw new Exception($"{nameof(settings.ServiceBusConnectionString)} is null, empty or whitespace");
            }
            if (settings.SendRequestQueueName == null)
            {
                throw new Exception($"{nameof(settings.SendRequestQueueName)} is null, empty or whitespace");
            }

            List<string> birthdays = new List<string>();

            for (int i = Math.Min(140, DateTime.Today.Year - 1900); i > -1; i--)
            {
                birthdays.Add
                (
                    DateTime.Today.AddYears(-i).ToString("M/d/yyyy")
                ); 
            }

            var query = new QueryExpression(ContactModel.LogicalName);
            query.Criteria.AddCondition(ContactModel.Fields.Birthday, ConditionOperator.In, birthdays.ToArray());

            var contactsCollection = Service.RetrieveMultiple(query).Entities;

            if (contactsCollection != null)
            {
                var contacts = contactsCollection.Select(p => p.Id).ToArray();

                foreach (var contact in contacts)
                {
                    var queueMessageSender = new QueueMessageSender(settings.ServiceBusConnectionString, _tracer);

                    queueMessageSender.SendMessage(contact.ToString(), settings.SendRequestQueueName);
                }
            }

            actionResponse.Status = Status.Success;

        }
    }
}