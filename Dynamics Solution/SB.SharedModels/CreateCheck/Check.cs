using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SB.SharedModels.CreateCheck
{
    [DataContract]
    public class Check
    {
        [DataMember(Name = "shop")]
        public string Shop { get; set; }
        [DataMember(Name = "client")]
        public Client Client { get; set; }
        [DataMember(Name = "products")]
        public List<Product> Products { get; set; }
    }
}