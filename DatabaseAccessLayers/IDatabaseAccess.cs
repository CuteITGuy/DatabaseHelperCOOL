using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DatabaseAccessLayers
{
    public interface IDatabaseAccess
    {
        #region Abstract
        void ExecuteCommand(string query, Action<SqlCommand> commandAction,
            CommandType commandType = CommandType.Text,
            IEnumerable<Parameter> parameters = null);

        void ExecuteReader(string query, Action<IDataRecord> readAction,
            CommandType commandType = CommandType.Text, IEnumerable<Parameter> parameters = null);

        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        #endregion
    }
}