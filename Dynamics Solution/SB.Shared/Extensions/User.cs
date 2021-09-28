using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;

namespace SB.Shared.Extensions
{
    public class User : UserModel
    {
        public User(IOrganizationService service) : base(service) { }
        public User(IOrganizationService service, Guid id) : base(id, service) { }
        public User(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public User(Entity entity, IOrganizationService service) : base(entity, service) { }
    }
}
