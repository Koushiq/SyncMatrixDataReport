using MFiles.VAF.Configuration;
using SyncMatrixDataReport.Formatter;
using System.Runtime.Serialization;

namespace SyncMatrixDataReport.Model
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
        [DataMember]
        public string PropertyValue { get; set; }
    }
}
