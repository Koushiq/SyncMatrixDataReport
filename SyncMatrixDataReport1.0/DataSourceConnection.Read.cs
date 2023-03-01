using System;
using System.Collections.Generic;
using MFiles.Extensibility.ExternalObjectTypes;
using MFiles.Extensibility.Framework.ExternalObjectTypes;
using SyncMatrixDataReport.Formatter;

namespace SyncMatrixDataReport1._0
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
            var models = JsonToModelFormatter.GetAllNodesAsModel();
            var dictionary = new Dictionary<int, object>();
            var ordinalNumber = 1;
            foreach (var model in models)
            {
                try
                {
                    if (model.PropertyType == "int")
                    {
                        dictionary.Add(ordinalNumber, Convert.ToInt32(model.PropertyValue) as object);
                    }
                    else
                    {
                        dictionary.Add(ordinalNumber, Convert.ToString(model.PropertyValue) as object);
                    }
                    ordinalNumber++;
                }
                catch (Exception ex)
                {

                }

            }
            var dataItemSimple = new DataItemSimple(dictionary);
            list.Add(dataItemSimple);
            return list;
        }
	}
}
