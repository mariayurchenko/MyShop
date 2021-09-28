using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;

namespace SB.Shared.EntityProviders
{
    public class CheckProduct : CheckProductModel
    {
        public CheckProduct(IOrganizationService service) : base(service) { }
        public CheckProduct(IOrganizationService service, Guid id) : base(id, service) { }
        public CheckProduct(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public CheckProduct(Entity entity, IOrganizationService service) : base(entity, service) { }
    }
}
