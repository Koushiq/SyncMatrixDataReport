using MFiles.VAF.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SyncMatrixDataReport.Model;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyncMatrixDataReport
{
	/// <summary>
	/// Configuration.
	/// </summary>
	[DataContract]
	[JsonConfEditor]
	public class ConfigurationRoot
	{
        [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        [DataMember]
        public IList<InitializationModel> Fields { get; set; }

        //[Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        //[DataMember]
        //public string VaultGuid { get; set; }

        [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        [DataMember]
        public MFIdentifier NameOrTitleGuid = "{3E2BB7EB-C49E-4C8C-825C-CAE0AEBA9A06}";
    }
}
