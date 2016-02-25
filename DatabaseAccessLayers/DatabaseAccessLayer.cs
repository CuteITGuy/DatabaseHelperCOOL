using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DatabaseAccessLayers
{
    public class DatabaseAccessLayer: IDatabaseAccess
    {
        #region  Constructors & Destructor
        public DatabaseAccessLayer(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        public DatabaseAccessLayer(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion


        #region  Properties & Indexers
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        #endregion


        #region Methods
        public void ExecuteCommand(string query, Action<SqlCommand> commandAction,
            CommandType commandType = CommandType.Text,
            IEnumerable<Parameter> parameters = null)
        {
            using (var connection = OpenConnection())
            {
                using (var command = new SqlCommand(query, connection) { CommandType = commandType })
                {
                    if (parameters == null) return;
                    foreach (var p in parameters)
                    {
                        command.Parameters.AddWithValue(p.Name, p.Value);
                    }
                    commandAction(command);
                }
            }
        }

        public void ExecuteReader(string query, Action<IDataRecord> readAction,
            CommandType commandType = CommandType.Text, IEnumerable<Parameter> parameters = null)
        {
            ExecuteCommand(query, cmd =>
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        readAction(reader);
                    }
                }
            }, commandType, parameters);
        }
        #endregion


        #region Implementation
        private string CreateConnectionString()
            => string.IsNullOrEmpty(DatabaseName)
                   ? ConnectionString
                   : new SqlConnectionStringBuilder(ConnectionString) { InitialCatalog = DatabaseName }.ToString();

        private SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(CreateConnectionString());
            connection.Open();
            return connection;
        }
        #endregion
    }
}