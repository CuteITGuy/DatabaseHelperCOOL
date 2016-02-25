using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;


namespace DatabaseModels
{
    public class DatabaseModelContext: DatabaseModelBase
    {
        #region Fields
        private const string SERVER_DATASOURCE_COL = "data_source";
        private const string SERVER_NAME_COL = "name";
        #endregion


        #region  Properties & Indexers
        public IEnumerable<Server> Servers { get; set; }
        #endregion


        #region Override
        protected override void OnLoad()
        {
            var query = $"SELECT {SERVER_NAME_COL}, {SERVER_DATASOURCE_COL} FROM sys.servers";
            var servers = new List<Server>();

            _databaseAccessLayer.ExecuteReader(query, reader => { servers.Add(ReadServerInfo(reader)); });
            Parallel.ForEach(servers, server => server.Load(_databaseAccessLayer));
            Servers = servers;
        }
        #endregion


        #region Implementation
        private static string ReadServerDataSource(IDataRecord reader)
        {
            return Convert.ToString(reader[SERVER_DATASOURCE_COL]);
        }

        private static Server ReadServerInfo(IDataRecord reader)
        {
            return new Server(ReadServerName(reader), ReadServerDataSource(reader));
        }

        private static string ReadServerName(IDataRecord reader)
        {
            return Convert.ToString(reader[SERVER_NAME_COL]);
        }
        #endregion
    }
}