using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class CheckProductModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "sb_checkproduct";

		#region Attribute Names
		public static class Fields
		{
			public const string
				Check = "sb_checkid",
				Cost = "sb_cost",
				Costwithdiscount = "sb_totalcost",
				Discount = "sb_discount",
				Number = "sb_number",
				PrimaryId = "sb_checkproductid",
				Product = "sb_productid";

			public static string[] All => new[] { Check,
				Cost,
				Costwithdiscount,
				Discount,
				Number,
				PrimaryId,
				Product };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public EntityReference Check
		{
			get => (EntityReference)this[Fields.Check];
			set => this[Fields.Check] = value; 
		}
		public decimal? Cost
		{
			get => (decimal?)this[Fields.Cost];
			set => this[Fields.Cost] = value; 
		}
		public decimal? Costwithdiscount
		{
			get => (decimal?)this[Fields.Costwithdiscount];
			set => this[Fields.Costwithdiscount] = value; 
		}
		public decimal? Discount
		{
			get => (decimal?)this[Fields.Discount];
			set => this[Fields.Discount] = value; 
		}
		public string Number
		{
			get => (string)this[Fields.Number];
			set => this[Fields.Number] = value; 
		}
		public EntityReference Product
		{
			get => (EntityReference)this[Fields.Product];
			set => this[Fields.Product] = value; 
		}
		#endregion

		#region Constructors
		protected CheckProductModel()
			: base(LogicalName) { }
		protected CheckProductModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected CheckProductModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected CheckProductModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected CheckProductModel(Entity entity, IOrganizationService service)
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