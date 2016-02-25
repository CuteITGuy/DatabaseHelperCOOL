using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;


namespace DatabaseModels
{
    public class Server: DatabaseModelBase
    {
        #region Fields
        private const string DATABASE_NAME_COL = "name";
        private IEnumerable<Database> _databases;
        private string _dataSource;
        private string _name;
        #endregion


        #region  Constructors & Destructor
        public Server(string name, string dataSource)
        {
            DataSource = dataSource;
            Name = name;
        }
        #endregion


        #region  Properties & Indexers
        public IEnumerable<Database> Databases
        {
            get { return _databases; }
            private set { SetProperty(ref _databases, value); }
        }

        public string DataSource
        {
            get { return _dataSource; }
            private set { SetProperty(ref _dataSource, value); }
        }

        public string Name
        {
            get { return _name; }
            private set { SetProperty(ref _name, value); }
        }
        #endregion


        #region Override
        protected override void OnLoad()
        {
            var query = $"SELECT {DATABASE_NAME_COL} FROM sys.databases";
            var databases = new List<Database>();

            _databaseAccessLayer.ExecuteReader(query, reader => { databases.Add(ReadDatabase(reader)); });
            Parallel.ForEach(databases, database => database.Load(_databaseAccessLayer));
            Databases = databases;
        }
        #endregion


        #region Implementation
        private static Database ReadDatabase(IDataRecord reader)
        {
            return new Database(ReadDatabaseName(reader));
        }

        private static string ReadDatabaseName(IDataRecord reader)
        {
            return Convert.ToString(reader[DATABASE_NAME_COL]);
        }
        #endregion
    }
}