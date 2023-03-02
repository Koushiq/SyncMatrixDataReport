using MFiles.VAF.Configuration;
using SyncMatrixReport.Formatter;
using System.Runtime.Serialization;

namespace SyncMatrixReport.Model
{
    [DataContract]
    public class InitializationModel
    {
        [DataMember]
        public string PropertyName { get; set; }
        [DataMember]
        [JsonConfEditor(TypeEditor = "options",
        Options = JsonToModelFormatter.AvaliableDataTypes)]
        public string PropertyType { get; set; }
        
    }
}
