using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;
using System;

namespace SB.Shared.EntityProviders
{
    public class Product : ProductModel
    {
        public Product(IOrganizationService service) : base(service) { }
        public Product(IOrganizationService service, Guid id) : base(id, service) { }
        public Product(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Product(Entity entity, IOrganizationService service) : base(entity, service) { }
    }
}
