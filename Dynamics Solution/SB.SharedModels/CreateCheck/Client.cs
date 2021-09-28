using System.Runtime.Serialization;


namespace SB.SharedModels.CreateCheck
{
    [DataContract]
    public class Client
    {
        [DataMember(Name = "loyaltycard")]
        public string LoyaltyCard { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
    }
}