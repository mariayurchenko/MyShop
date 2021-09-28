using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class ContactModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "contact";

		#region Attribute Names
		public static class Fields
		{
			public const string
				Birthday = "birthdate",
				CreatedOn = "createdon",
				Email = "emailaddress1",
				FirstName = "firstname",
				LastName = "lastname",
				MobilePhone = "mobilephone",
				PrimaryId = "contactid",
				PrimaryName = "fullname";

			public static string[] All => new[] { Birthday,
				CreatedOn,
				Email,
				FirstName,
				LastName,
				MobilePhone,
				PrimaryId,
				PrimaryName };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public DateTime? Birthday
		{
			get => (DateTime?)this[Fields.Birthday];
			set => this[Fields.Birthday] = value; 
		}
		public DateTime? CreatedOn
		{
			get => (DateTime?)this[Fields.CreatedOn];
			set => this[Fields.CreatedOn] = value; 
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
		public string MobilePhone
		{
			get => (string)this[Fields.MobilePhone];
			set => this[Fields.MobilePhone] = value; 
		}
		#endregion

		#region Constructors
		protected ContactModel()
			: base(LogicalName) { }
		protected ContactModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected ContactModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected ContactModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected ContactModel(Entity entity, IOrganizationService service)
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