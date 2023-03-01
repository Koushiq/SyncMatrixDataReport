using Newtonsoft.Json;
using SyncMatrixDataReport.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncMatrixDataReport.Formatter
{
    public class ApiResponseModel
    {
        [JsonProperty("entities")]
        public IList<object> Entities { get; set; }
    }
    public class JsonToModelFormatter
    {
        private static Dictionary<string, string> propertyTypeMapping = new Dictionary<string, string>()
        {
            { "AANID","int" },
            { "LAND","string" },
            { "PLZ","string" },
            { "PTT_ZUSATZ","int" },
            { "ORT","string" },

        };
        public const string AvaliableDataTypes = "{selectOptions:[\"int\",\"string\"]}";


        private static string GetPropertyTypeByKey(string property)
        {
            var value = propertyTypeMapping[property];
            return value;
        }

        public static List<InitializationModel> GetAllNodesAsModel()
        {
            var pathToJsonFile = "Assests/add_report_01_api_response.json";
            var list = new List<InitializationModel>();
            pathToJsonFile = System.AppDomain.CurrentDomain.BaseDirectory + pathToJsonFile;
            try
            {
                if (File.Exists(pathToJsonFile))
                {
                    var str = File.ReadAllText(pathToJsonFile);
                    var deserializedValue = JsonConvert.DeserializeObject<ApiResponseModel>(str);
                    var nodes = deserializedValue.Entities.ToList();

                    foreach (var node in nodes)
                    {
                        var currentNode = JsonConvert.DeserializeObject<Dictionary<string, string>>(node.ToString());
                        foreach (var kp in currentNode)
                        {
                            list.Add(new InitializationModel()
                            {
                                PropertyName = kp.Key,
                                PropertyType = GetPropertyTypeByKey(kp.Key),
                                PropertyValue = kp.Value
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return list;
        }
        public static List<InitializationModel> GetFirstNode()
        {
            var pathToJsonFile = "Assests/add_report_01_api_response.json";
            var list = new List<InitializationModel>();
            pathToJsonFile = System.AppDomain.CurrentDomain.BaseDirectory + pathToJsonFile;
            try
            {
                if (File.Exists(pathToJsonFile))
                {
                    var str = File.ReadAllText(pathToJsonFile);
                    var deserializedValue = JsonConvert.DeserializeObject<ApiResponseModel>(str);
                    var firstNode = deserializedValue.Entities.FirstOrDefault();

                    if (firstNode != null)
                    {
                        var firstNodeDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(firstNode.ToString());
                        foreach (var kp in firstNodeDictionary)
                        {
                            list.Add(new InitializationModel()
                            {
                                PropertyName = kp.Key,
                                PropertyType = GetPropertyTypeByKey(kp.Key),
                                PropertyValue = kp.Value
                            });
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return list;
        }
    }
}
