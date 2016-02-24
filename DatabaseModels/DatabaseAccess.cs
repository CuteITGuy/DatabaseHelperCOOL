using System;
using System.Collections.Generic;
using System.Data;
using DatabaseAccessLayers;


namespace DatabaseModels
{
    public class DatabaseAccess
    {
        #region Fields
        private const string DATABASE_NAME_COL = "name";
        private const string SERVER_DATASOURCE_COL = "data_source";
        private const string SERVER_NAME_COL = "name";
        private readonly DatabaseAccessLayer _databaseAccessLayer;
        #endregion


        #region  Constructors & Destructor
        public DatabaseAccess(string connectionString)
        {
            _databaseAccessLayer = new DatabaseAccessLayer(connectionString);
        }

        public DatabaseAccess(string connectionString, string databaseName)
        {
            _databaseAccessLayer = new DatabaseAccessLayer(connectionString, databaseName);
        }
        #endregion


        #region Methods
        public IEnumerable<string> GetDatabaseNames()
        {
            var query = $"SELECT {DATABASE_NAME_COL} FROM sys.databases";
            var databaseNames = new List<string>();

            _databaseAccessLayer.ExecuteReader(query, reader => { databaseNames.Add(ReadDatabaseName(reader)); });
            return databaseNames;
        }

        public IEnumerable<Table> GetTable

        public IEnumerable<Server> GetServerInfos()
        {
            var query = $"SELECT {SERVER_NAME_COL}, {SERVER_DATASOURCE_COL} FROM sys.servers";
            var serverInfos = new List<Server>();

            _databaseAccessLayer.ExecuteReader(query, reader => { serverInfos.Add(ReadServerInfo(reader)); });
            return serverInfos;
        }
        #endregion


        #region Implementation
        private static string ReadDatabaseName(IDataRecord reader)
        {
            return Convert.ToString(reader[DATABASE_NAME_COL]);
        }

        private static Server ReadServerInfo(IDataRecord reader)
        {
            return new Server
            {
                Name = Convert.ToString(reader[SERVER_NAME_COL]),
                DataSource = Convert.ToString(reader[SERVER_DATASOURCE_COL])
            };
        }
        #endregion
    }
}