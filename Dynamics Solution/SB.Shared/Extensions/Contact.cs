using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;

namespace SB.Shared.EntityProviders
{
    public class Contact : ContactModel
    {
        public Contact(IOrganizationService service) : base(service) { }
        public Contact(IOrganizationService service, Guid id) : base(id, service) { }
        public Contact(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Contact(Entity entity, IOrganizationService service) : base(entity, service) { }
    }
}
