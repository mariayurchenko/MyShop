using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class AutonumeratorModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "sb_autonumerator";

		#region Attribute Names
		public static class Fields
		{
			public const string
				CurrentNumber = "sb_currentnumber",
				EntityName = "sb_entity",
				Prefix = "sb_prefix",
				PrimaryId = "sb_autonumeratorid";

			public static string[] All => new[] { CurrentNumber,
				EntityName,
				Prefix,
				PrimaryId };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public int? CurrentNumber
		{
			get => (int?)this[Fields.CurrentNumber];
			set => this[Fields.CurrentNumber] = value; 
		}
		public string EntityName
		{
			get => (string)this[Fields.EntityName];
			set => this[Fields.EntityName] = value; 
		}
		public string Prefix
		{
			get => (string)this[Fields.Prefix];
			set => this[Fields.Prefix] = value; 
		}
		#endregion

		#region Constructors
		protected AutonumeratorModel()
			: base(LogicalName) { }
		protected AutonumeratorModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected AutonumeratorModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected AutonumeratorModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected AutonumeratorModel(Entity entity, IOrganizationService service)
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