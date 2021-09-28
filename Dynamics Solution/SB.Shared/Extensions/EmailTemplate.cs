using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;

namespace SB.Shared.Extensions
{
    public class EmailTemplate : EmailTemplateModel
    {
        public EmailTemplate(IOrganizationService service) : base(service) { }
        public EmailTemplate(IOrganizationService service, Guid id) : base(id, service) { }
        public EmailTemplate(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public EmailTemplate(Entity entity, IOrganizationService service) : base(entity, service) { }
    }
}
