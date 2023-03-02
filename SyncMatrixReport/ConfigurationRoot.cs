using MFiles.VAF.Configuration;
using SyncMatrixReport.Model;
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
        [DataMember]
        public List<InitializationModel> AvaliableColums { get; set; }
    }
}
