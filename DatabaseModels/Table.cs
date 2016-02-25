using System;
using System.Collections.Generic;
using System.Data;
using DatabaseAccessLayers;


namespace DatabaseModels
{
    public class Table: DatabaseModelBase
    {
        #region Fields
        private const string COLUMN_DEFAULT_COL = "COLUMN_DEFAULT";
        private const string COLUMN_NAME_COL = "COLUMN_NAME";
        private const string DATA_TYPE_COL = "DATA_TYPE";
        private const string IS_NULLABLE_COL = "IS_NULLABLE";
        private const string MAXIMUM_LENGTH_COLUMN = "CHARACTER_MAXIMUM_LENGTH";
        private const string TABLE_NAME_COL = "TABLE_NAME";
        private IEnumerable<Column> _columns;
        private string _name;
        #endregion


        #region  Constructors & Destructor
        public Table(string name)
        {
            Name = name;
        }
        #endregion


        #region  Properties & Indexers
        public IEnumerable<Column> Columns
        {
            get { return _columns; }
            private set { SetProperty(ref _columns, value); }
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
            var query =
                $@" SELECT {COLUMN_NAME_COL}, {COLUMN_DEFAULT_COL}, {IS_NULLABLE_COL}, {DATA_TYPE_COL}, {MAXIMUM_LENGTH_COLUMN}
                    FROM    INFORMATION_SCHEMA.COLUMNS
                    WHERE   {TABLE_NAME_COL} = @{TABLE_NAME_COL}
                    ORDER   BY ORDINAL_POSITION";

            var columns = new List<Column>();
            _databaseAccessLayer.ExecuteReader(query, reader => columns.Add(ReadColumn(reader)), CommandType.Text,
                new[] { new Parameter { Name = $"@{TABLE_NAME_COL}", Value = Name } });
            Columns = columns;
        }
        #endregion


        #region Implementation
        private static Column ReadColumn(IDataRecord reader)
        {
            return new Column
            {
                Name = ReadColumnName(reader),
                DefaultValue = ReadColumnDefault(reader),
                DataType = ReadColumnDataType(reader),
                IsNullable = ReadColumnIsNullable(reader)
            };
        }

        private static DataType ReadColumnDataType(IDataRecord reader)
        {
            var typeString = Convert.ToString(reader[DATA_TYPE_COL]);
            var dbType = (DbType)Enum.Parse(typeof(DbType), typeString, true);

            var maxLengthValue = reader[MAXIMUM_LENGTH_COLUMN];

            return new DataType
            {
                Type = dbType,
                MaximumCharacterLength = maxLengthValue is DBNull ? (int?)null : Convert.ToInt32(maxLengthValue)
            };
        }

        private static object ReadColumnDefault(IDataRecord reader)
        {
            return reader[COLUMN_DEFAULT_COL];
        }

        private static bool ReadColumnIsNullable(IDataRecord reader)
        {
            return Convert.ToString(reader[IS_NULLABLE_COL]).Equals("YES", StringComparison.InvariantCultureIgnoreCase);
        }

        private static string ReadColumnName(IDataRecord reader)
        {
            return Convert.ToString(reader[COLUMN_NAME_COL]);
        }
        #endregion
    }
}