using MFiles.VAF.Configuration;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyncMatrixReport
{
    /// <summary>
    /// Configuration.
    /// </summary>
    [DataContract]
    [JsonConfEditor]
    public class ConfigurationRoot
    {
        //[DataMember]
        //public Dictionary<MFIdentifier,string> AvaliableProperties { get; set; } 
    }
}
