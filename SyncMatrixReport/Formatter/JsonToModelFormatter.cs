using Newtonsoft.Json;
using SyncMatrixReport.Model;
using SyncMatrixReport.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SyncMatrixReport.Formatter
{
    public class JsonToModelFormatter
    {
        private static Dictionary<string, string> propertyTypeMapping;
        //= new Dictionary<string, string>() {
        //    {"AANID","int" },
        //    {"LAND","string" },
        //    {"PLZ","string" },
        //    {"PTT_ZUSATZ","int" },
        //    {"ORT","string" }
        //};
        private static string PATH_TO_RESPONSE_FILE = "Assests/add_report_01_api_response.json";
        private static string PATH_TO_PROPERTYTYPEMAPPING_FILE = "Assests/PropertyTypeMapping.json";
        public const string AvaliableDataTypes = "{selectOptions:[\"int\",\"string\",\"date-time\"]}";
        private static List<Model.InitializationModel> initializationModels;
        public static void SetAvaliableColumnsInFormatter(List<Model.InitializationModel> models)
        {
            initializationModels = models;
        }

        public static string GetPropertyTypeByKey(string property)
        {
            if (propertyTypeMapping == null && initializationModels != null && initializationModels.Any())
            {
                propertyTypeMapping = new Dictionary<string, string>();

                foreach (var model in initializationModels)
                {
                    propertyTypeMapping.Add(model.PropertyName, model.PropertyType);
                }
            }
            else
            {
                propertyTypeMapping = GetPropertyTypeMappingFromJsonFile();
            }
            var value = propertyTypeMapping[property];
            return value;
        }
        private static Dictionary<string,string> GetPropertyTypeMappingFromJsonFile()
        {
            var pathToJsonFile = PATH_TO_PROPERTYTYPEMAPPING_FILE;
            var dictionary = new Dictionary<string, string>();
            pathToJsonFile = System.AppDomain.CurrentDomain.BaseDirectory + pathToJsonFile;
            try
            {
                if(File.Exists(pathToJsonFile))
                {
                    var str = File.ReadAllText(pathToJsonFile);
                    dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
                }
            }
            catch(Exception ex)
            {
                
            }
            return dictionary;
        }
        public static EntityListModel GetAllNodesAsModel()
        {
            var pathToJsonFile = PATH_TO_RESPONSE_FILE;
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
            var pathToJsonFile = PATH_TO_RESPONSE_FILE;
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
