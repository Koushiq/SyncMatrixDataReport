using Newtonsoft.Json;
using SyncMatrixReport.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SyncMatrixReport.Formatter
{
    public class JsonToModelFormatter
    {
        private static Dictionary<string, string> propertyTypeMapping= new Dictionary<string, string>() {
            {"AANID","int" },
            {"LAND","sttring" },
            {"PLZ","string" },
            {"PTT_ZUSATZ","int" },
            {"ORT","string" }
        };
        public const string AvaliableDataTypes = "{selectOptions:[\"int\",\"string\"]}";


        public static string GetPropertyTypeByKey(string property)
        {
            //if(propertyTypeMapping == null)
            //{
            //    propertyTypeMapping = SetPropertyTypeMapping();
            //}
            var value = propertyTypeMapping[property];
            return value;
        }

        public static EntityListModel GetAllNodesAsModel()
        {
            var pathToJsonFile = "Assests/add_report_01_api_response.json";
            pathToJsonFile = System.AppDomain.CurrentDomain.BaseDirectory + pathToJsonFile;
            var model = new EntityListModel(); 
            try
            {
                if (File.Exists(pathToJsonFile))
                {
                    var str = File.ReadAllText(pathToJsonFile);
                    var deserializedValue = JsonConvert.DeserializeObject<ApiResponseModel>(str);
                    var entities = deserializedValue.Entities.ToList();
                    
                    foreach (var entity in entities)
                    {
                        var deserializedEntity = JsonConvert.DeserializeObject<Dictionary<string, string>>(entity.ToString());
                        var dictionary = new Dictionary<string, string>();
                        foreach (var prop in deserializedEntity)
                        {
                            dictionary.Add(prop.Key, prop.Value);
                        }
                        model.Entities.Add(new EntityModel() { Entity = dictionary });

                    }
                }
            }
            catch (Exception ex)
            {

            }

            return model;
        }
        private static Dictionary<string,string> SetPropertyTypeMapping()
        {
            var firstNode = GetFirstNode();
            var dictionary = new Dictionary<string, string>();
            foreach (var prop in firstNode)
            {
                dictionary.Add(prop.PropertyName,prop.PropertyType);
            }
            return dictionary;
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
