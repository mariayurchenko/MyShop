using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;
using System;

namespace SB.Shared.Extensions
{
    public class Autonumerator : AutonumeratorModel
    {
        public Autonumerator(IOrganizationService service) : base(service) { }
        public Autonumerator(IOrganizationService service, Guid id) : base(id, service) { }
        public Autonumerator(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Autonumerator(Entity entity, IOrganizationService service) : base(entity, service) { }
    }
}
