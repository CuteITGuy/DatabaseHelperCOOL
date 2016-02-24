using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DatabaseAccessLayers
{
    public class DatabaseAccessLayer
    {
        #region Fields
        private readonly string _connectionString;
        private readonly string _databaseName;
        #endregion


        #region  Constructors & Destructor
        public DatabaseAccessLayer(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public DatabaseAccessLayer(string connectionString)
        {
            _connectionString = connectionString;
        }
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
            => string.IsNullOrEmpty(_databaseName)
                   ? _connectionString
                   : new SqlConnectionStringBuilder(_connectionString) { InitialCatalog = _databaseName }.ToString();

        private SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(CreateConnectionString());
            connection.Open();
            return connection;
        }
        #endregion
    }
}