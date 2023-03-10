using System;
using System.Collections.Generic;
using System.Linq;
using MFiles.Extensibility.ExternalObjectTypes;

namespace SyncMatrixDataReport1._0
{
	/// <summary>
	/// An implementation of IExternalObjectTypeConnection,
	/// using a JsonDataProvider for the actual data storage/retrieval.
	/// Parts for updating data.
	/// </summary>
	public partial class DataSourceConnection
	{
		/// <summary>
		/// Returns whether the data source supports updating the data.
		/// </summary>
		/// <returns>Returns true if the data source supports updating the values.</returns>
		public override bool CanUpdate()
		{
			// TODO: Return true if this connection supports updating items in the source system.
			return false;
		}

		/// <summary>
		/// Validates that the update statement is valid in this data source.
		/// </summary>
		/// <param name="updatedColumns">Columns that have been marked for updating</param>
		/// <returns>True if the update statement is valid</returns>
		public override bool ValidateUpdateStatement( List<ColumnDefinition> updatedColumns )
		{
			// TODO: Validate the update statement, if appropriate.
			return true;
		}

		/// <summary>
		/// Updates the item identified in extid.
		/// </summary>
		/// <param name="extid">The item to update</param>
		/// <param name="values">The values to update</param>
		/// <param name="previousValues">The previous values of the object including all the imported values.
		/// Use these to detect possible updated conflicts.</param>
		public override void UpdateItem(
			string extid,
			List<ColumnValue> values,
			List<ColumnValue> previousValues
		)
		{
			// TODO: Update the item.
			throw new NotImplementedException();
		}
	}
}
