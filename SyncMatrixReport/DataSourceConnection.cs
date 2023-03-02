using MFiles.Extensibility.ExternalObjectTypes;
using MFiles.Extensibility.Framework.ExternalObjectTypes;
using SyncMatrixReport.Formatter;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SyncMatrixReport
{
    /// <summary>
    /// An implementation of IExternalObjectTypeConnection,
    /// using a JsonDataProvider for the actual data storage/retrieval.
    /// </summary>
    /// 
    // Note that implementations for reading data, inserting data, updating data, and deleting data
    // have been split into separate partial classes:
    // DataSourceConnection.cs (helper methods)
    // DataSourceConnection.Read.cs (methods for reading data)
    // DataSourceConnection.Insert.cs (methods for inserting data)
    // DataSourceConnection.Update.cs (methods for updating data)
    // DataSourceConnection.Delete.cs (methods for deleting data)
    public partial class DataSourceConnection
        : ExternalObjectTypeConnectionBase
    {
        /// <summary>
        /// Connection configuration.
        /// </summary>
        protected ExternalObjectTypeConfiguration<ConfigurationRoot> Config { get; set; }

        /// <summary>
        /// Map of managed types to ColumnType.
        /// </summary>
        private static readonly Dictionary<Type, ColumnType> typeMappingByType = new Dictionary<Type, ColumnType>() {
                { typeof( string ), ColumnType.DBTYPE_WSTR },
                { typeof( int ), ColumnType.DBTYPE_I4 },
                { typeof( bool ), ColumnType.DBTYPE_BOOL },
                { typeof( double ), ColumnType.DBTYPE_DECIMAL },
                { typeof( DateTime ), ColumnType.DBTYPE_DBDATE } };

        /// <summary>
        /// Instantiates a DataSourceConnection for a specific data source.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="stopToken">Stop token.</param>
        public DataSourceConnection(
            ExternalObjectTypeConfiguration<ConfigurationRoot> config,
            CancellationToken stopToken
        ) : base(config, stopToken)
        {
            // Set.
            this.Config = config;
            this.Initialize();
        }
        #region utility
        private void Initialize()
        {
            var firstNode = JsonToModelFormatter.GetFirstNode();
            this.Config.CustomConfiguration.AvaliableColums = firstNode;
            JsonToModelFormatter.SetAvaliableColumnsInFormatter(this.Config.CustomConfiguration.AvaliableColums);
        }
        #endregion
        /// <summary>
        /// Close connection.
        /// </summary>
        public override void CloseConnectionImpl()
        {
            
            // TODO: Close any connection, if needed.
        }
    }
}
