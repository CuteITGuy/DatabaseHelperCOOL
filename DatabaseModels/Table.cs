using System.Collections.Generic;
using CB.Model.Common;


namespace DatabaseModels
{
    public class Table: ObservableObject
    {
        #region Fields
        private IEnumerable<Column> _columns;

        private string _name;
        #endregion


        #region  Properties & Indexers
        public IEnumerable<Column> Columns
        {
            get { return _columns; }
            set { SetProperty(ref _columns, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        #endregion
    }
}