namespace SB.Shared.Models.Actions
{
    public class ActionNames
    {
        public const string ActionTracking = "mar_ActionTracking";
        public class ActionTrackingNames
        {
            public const string CreateCheck = nameof(CreateCheck);
            public const string SendLoyaltyCardForm = nameof(SendLoyaltyCardForm);
            public const string SendBirthdayEmail = nameof(SendBirthdayEmail);
            public const string RetrieveBirthdayContacts = nameof(RetrieveBirthdayContacts);
        }
    }
}