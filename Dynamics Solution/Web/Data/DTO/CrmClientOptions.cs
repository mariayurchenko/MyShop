using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class CrmClientOptions
    {
        public string ApiVersion { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
       // public string DirectoryId { get; set; }
        public string Resource { get; set; }
        public string TenantId { get; set; }
    }
}
