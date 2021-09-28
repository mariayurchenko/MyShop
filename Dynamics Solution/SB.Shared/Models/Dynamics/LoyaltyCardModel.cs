using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class LoyaltyCardModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "sb_loyaltycard";

		#region Attribute Names
		public static class Fields
		{
			public const string
				Client = "sb_contactid",
				Email = "sb_email",
				FirstName = "sb_firstname",
				LastName = "sb_lastname",
				Number = "sb_number",
				Phone = "sb_phone",
				PrimaryId = "sb_loyaltycardid";

			public static string[] All => new[] { Client,
				Email,
				FirstName,
				LastName,
				Number,
				Phone,
				PrimaryId };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public EntityReference Client
		{
			get => (EntityReference)this[Fields.Client];
			set => this[Fields.Client] = value; 
		}
		public string Email
		{
			get => (string)this[Fields.Email];
			set => this[Fields.Email] = value; 
		}
		public string FirstName
		{
			get => (string)this[Fields.FirstName];
			set => this[Fields.FirstName] = value; 
		}
		public string LastName
		{
			get => (string)this[Fields.LastName];
			set => this[Fields.LastName] = value; 
		}
		public string Number
		{
			get => (string)this[Fields.Number];
			set => this[Fields.Number] = value; 
		}
		public string Phone
		{
			get => (string)this[Fields.Phone];
			set => this[Fields.Phone] = value; 
		}
		#endregion

		#region Constructors
		protected LoyaltyCardModel()
			: base(LogicalName) { }
		protected LoyaltyCardModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected LoyaltyCardModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected LoyaltyCardModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected LoyaltyCardModel(Entity entity, IOrganizationService service)
			: base(entity, service) { }
		#endregion

		#region Public Methods
		public override string GetPrimaryAttribute()
        {
            return Fields.PrimaryId;
        }
		#endregion
	}
}