using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DatabaseAccessLayers;


namespace DatabaseModels
{
    public class Database: DatabaseModelBase
    {
        #region Fields
        private const string TABLE_NAME_COL = "TABLE_NAME";
        private string _name;
        private IEnumerable<Table> _tables;
        #endregion


        #region  Constructors & Destructor
        public Database(string name)
        {
            Name = name;
        }
        #endregion


        #region  Properties & Indexers
        public string Name
        {
            get { return _name; }
            private set { SetProperty(ref _name, value); }
        }

        public IEnumerable<Table> Tables
        {
            get { return _tables; }
            private set { SetProperty(ref _tables, value); }
        }
        #endregion


        #region Override
        protected override void OnInitializeDataAccessLayer(IDatabaseAccess databaseAccessLayer)
        {
            databaseAccessLayer.DatabaseName = Name;
            base.OnInitializeDataAccessLayer(databaseAccessLayer);
        }

        protected override void OnLoad()
        {
            var query = $"SELECT {TABLE_NAME_COL} FROM INFORMATION_SCHEMA.TABLES";
            var tables = new List<Table>();

            _databaseAccessLayer.ExecuteReader(query, reader => tables.Add(ReadTable(reader)));
            Parallel.ForEach(tables, table => table.Load(_databaseAccessLayer));
            Tables = tables;
        }
        #endregion


        #region Implementation
        private static Table ReadTable(IDataRecord reader)
        {
            return new Table(ReadTableName(reader));
        }

        private static string ReadTableName(IDataRecord reader)
        {
            return Convert.ToString(reader[TABLE_NAME_COL]);
        }
        #endregion
    }
}