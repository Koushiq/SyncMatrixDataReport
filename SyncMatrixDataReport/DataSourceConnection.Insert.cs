using System;
using System.Collections.Generic;
using System.Linq;
using MFiles.Extensibility.ExternalObjectTypes;

namespace SyncMatrixDataReport
{
	/// <summary>
	/// An implementation of IExternalObjectTypeConnection,
	/// using a JsonDataProvider for the actual data storage/retrieval.
	/// Parts for inserting data.
	/// </summary>
	public partial class DataSourceConnection
	{
		/// <summary>
		/// True if the data source can return id of the inserted item.
		/// </summary>
		/// <returns>True if the data source supports returning the id of the inserted item.</returns>
		public override bool CanReturnIdOnInsert()
		{
            // TODO: Return true if this connection supports returning the ID on insert.
            return false;

        }

        /// <summary>
        /// True if the data source supports inserting.
        /// </summary>
        public override bool CanInsert()
		{
			// TODO: Return true if this connection supports inserting.
			return false;
		}

		/// <summary>
		/// Validates that the insert statement is valid in this data source.
		/// </summary>
		/// <param name="insertedColumns">Columns that have been marked for insert</param>
		/// <returns>True if the insert statements is valid for this data source.</returns>
		public override bool ValidateInsertStatement( List<ColumnDefinition> insertedColumns )
		{
			// Validate the insert statement, if appropriate.
			throw new NotImplementedException();
		}

		public override string InsertItem(List<ColumnValue> values, string title)
		{
			// Insert the item.
			throw new NotImplementedException();
		}
	}
}
