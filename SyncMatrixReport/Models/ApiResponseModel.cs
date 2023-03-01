using Newtonsoft.Json;
using System.Collections.Generic;

namespace SyncMatrixReport.Formatter
{
    public class ApiResponseModel
    {
        public ApiResponseModel()
        {
            Entities = new List<object>();
        }
        [JsonProperty("entities")]
        public IList<object> Entities { get; set; }
    }
    public class EntityListModel
    {
        public EntityListModel()
        {
            Entities = new List<EntityModel>();
        }
        public List<EntityModel> Entities { get; set; }
    }
    public class EntityModel
    {
        public Dictionary<string, string> Entity { get; set; }
    }
}
