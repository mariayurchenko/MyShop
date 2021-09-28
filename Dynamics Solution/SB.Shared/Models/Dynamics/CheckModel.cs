using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class CheckModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "sb_check";

		#region Attribute Names
		public static class Fields
		{
			public const string
				Client = "sb_client",
				EmailAddress = "sb_email",
				Number = "sb_number",
				PrimaryId = "sb_checkid",
				Shop = "sb_shopid",
				Totalamountcost = "sb_totalamount",
				Totaldiscountcost = "sb_totaldiscountcost";

			public static string[] All => new[] { Client,
				EmailAddress,
				Number,
				PrimaryId,
				Shop,
				Totalamountcost,
				Totaldiscountcost };
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
		public string EmailAddress
		{
			get => (string)this[Fields.EmailAddress];
			set => this[Fields.EmailAddress] = value; 
		}
		public string Number
		{
			get => (string)this[Fields.Number];
			set => this[Fields.Number] = value; 
		}
		public EntityReference Shop
		{
			get => (EntityReference)this[Fields.Shop];
			set => this[Fields.Shop] = value; 
		}
		public decimal? Totalamountcost
		{
			get => (decimal?)this[Fields.Totalamountcost];
			set => this[Fields.Totalamountcost] = value; 
		}
		public decimal? Totaldiscountcost
		{
			get => (decimal?)this[Fields.Totaldiscountcost];
			set => this[Fields.Totaldiscountcost] = value; 
		}
		#endregion

		#region Constructors
		protected CheckModel()
			: base(LogicalName) { }
		protected CheckModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected CheckModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected CheckModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected CheckModel(Entity entity, IOrganizationService service)
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