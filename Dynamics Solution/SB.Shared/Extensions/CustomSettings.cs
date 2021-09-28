using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;

namespace SB.Shared.Extensions
{
    public class CustomSettings : CustomSettingsModel
    {
        public CustomSettings(IOrganizationService service) : base(service) { }
        public CustomSettings(IOrganizationService service, Guid id) : base(id, service) { }
        public CustomSettings(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public CustomSettings(Entity entity, IOrganizationService service) : base(entity, service) { }
        
        public CustomSettings GetSettings(params string[] columns)
        {
            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(columns)
            };

            var settings = _service.RetrieveMultiple(query).Entities.Select(entity => new CustomSettings(entity, _service)).FirstOrDefault();

            if (settings == null)
            {
                throw new InvalidOperationException("SB Custom settings not found. Please configure system or contact the system administrator for support.");
            }

            return settings;
        }
    }
}