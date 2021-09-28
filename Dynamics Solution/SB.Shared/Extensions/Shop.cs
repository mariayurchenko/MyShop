using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;

namespace SB.Shared.Extensions
{
    public class Shop : ShopModel
    {
        public Shop(IOrganizationService service) : base(service) { }
        public Shop(IOrganizationService service, Guid id) : base(id, service) { }
        public Shop(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Shop(Entity entity, IOrganizationService service) : base(entity, service) { }
    }
}
