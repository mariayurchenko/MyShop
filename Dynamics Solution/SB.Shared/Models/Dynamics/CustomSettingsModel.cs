using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class CustomSettingsModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "sb_customsettings";

		#region Attribute Names
		public static class Fields
		{
			public const string
				PrimaryId = "sb_customsettingsid",
				PrimaryName = "sb_name",
				SendRequestQueueName = "sb_sendrequestqueuename",
				ServiceBusConnectionString = "sb_servicebusconnectionstring",
				Usertosendemail = "sb_usertosendemail";

			public static string[] All => new[] { PrimaryId,
				PrimaryName,
				SendRequestQueueName,
				ServiceBusConnectionString,
				Usertosendemail };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public string SendRequestQueueName
		{
			get => (string)this[Fields.SendRequestQueueName];
			set => this[Fields.SendRequestQueueName] = value; 
		}
		public string ServiceBusConnectionString
		{
			get => (string)this[Fields.ServiceBusConnectionString];
			set => this[Fields.ServiceBusConnectionString] = value; 
		}
		public EntityReference Usertosendemail
		{
			get => (EntityReference)this[Fields.Usertosendemail];
			set => this[Fields.Usertosendemail] = value; 
		}
		#endregion

		#region Constructors
		protected CustomSettingsModel()
			: base(LogicalName) { }
		protected CustomSettingsModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected CustomSettingsModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected CustomSettingsModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected CustomSettingsModel(Entity entity, IOrganizationService service)
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