using MFiles.Extensibility.ExternalObjectTypes;
using SyncMatrixReport.Formatter;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SyncMatrixReport
{
    /// <summary>
    /// An implementation of IExternalObjectTypeConnection,
    /// using a JsonDataProvider for the actual data storage/retrieval.
    /// Parts for reading data.
    /// </summary>
    public partial class DataSourceConnection
    {
        /// <summary>
        /// Gets the available columns from the currently open selection.
        /// </summary>
        /// <returns>Collection of available columns.</returns>
        public override List<ColumnDefinition> GetAvailableColumns()
        {
            // TODO: Return the column definitions that are supported for the current configuration.
            // NOTE: You may need to use "this.Config.CustomConfiguration" to see what's configured.
            var fields = JsonToModelFormatter.GetFirstNode();
            var list = new List<ColumnDefinition>();
            var ordinalCounter = 1;

            foreach (var field in fields)
            {
                var type = field.PropertyType == "int" ? ColumnType.DBTYPE_NUMERIC : ColumnType.DBTYPE_WCHAR;
                var columnDefination = new ColumnDefinition();
                columnDefination.Name = field.PropertyName;
                columnDefination.Ordinal = ordinalCounter++;
                columnDefination.Type = type;
                list.Add(columnDefination);
            }
            return list;
        }

        /// <summary>
        /// Gets the items as specified by the select statement.
        /// </summary>
        /// <returns>Items from the data source</returns>
        public override IEnumerable<DataItem> GetItems()
        {
            // TODO: Return actual items.
            // NOTE: You may need to use "this.Config.CustomConfiguration" to see what's configured.
            // NOTE: You will probably want to return instances of DataItemSimple.
            var list = new List<DataItemSimple>();
            var model = JsonToModelFormatter.GetAllNodesAsModel();
            
            foreach (var entity in model.Entities)
            {
                var dictionary = new Dictionary<int, object>();
                var ordinal = 1;
                foreach (var kp in entity.Entity)
                {
                    var type = JsonToModelFormatter.GetPropertyTypeByKey(kp.Key);
                    if(type == "int")
                    {
                        dictionary.Add(ordinal++ ,Convert.ToInt32(kp.Value) as object);
                    }
                    else
                    {
                        dictionary.Add(ordinal++, Convert.ToString(kp.Value) as object);
                    }
                }
                list.Add(new DataItemSimple(dictionary));
            }

            return list;

        }
    }
}
