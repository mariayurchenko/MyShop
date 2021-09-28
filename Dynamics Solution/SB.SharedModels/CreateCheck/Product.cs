using System.Runtime.Serialization;

namespace SB.SharedModels.CreateCheck
{
    [DataContract]
    public class Product 
    {
        [DataMember(Name = "article")]
        public string Article { get; set; }
        [DataMember(Name = "cost")]
        public decimal Cost { get; set; }
        [DataMember(Name = "discount")]
        public decimal Discount { get; set; }

    }
}