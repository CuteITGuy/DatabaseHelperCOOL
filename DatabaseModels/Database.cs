using System.Collections.Generic;
using CB.Model.Common;


namespace DatabaseModels
{
    public class Database: ObservableObject
    {
        #region Fields
        private string _name;

        private IEnumerable<Table> _tables;
        #endregion


        #region  Properties & Indexers
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public IEnumerable<Table> Tables
        {
            get { return _tables; }
            set { SetProperty(ref _tables, value); }
        }
        #endregion
    }
}