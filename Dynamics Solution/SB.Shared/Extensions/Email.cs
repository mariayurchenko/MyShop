using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;

namespace SB.Shared.Extensions
{
    public class Email : EmailModel
    {
        public Email(IOrganizationService service) : base(service) { }
        public Email(IOrganizationService service, Guid id) : base(id, service) { }
        public Email(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Email(Entity entity, IOrganizationService service) : base(entity, service) { }
    }
}
