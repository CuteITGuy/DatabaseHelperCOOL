using System.Collections.Generic;
using CB.Model.Common;


namespace DatabaseModels
{
    public class Server: ObservableObject
    {
        #region Fields
        private IEnumerable<Database> _databases;

        private string _dataSource;
        private string _name;
        #endregion


        #region  Properties & Indexers
        public IEnumerable<Database> Databases
        {
            get { return _databases; }
            set { SetProperty(ref _databases, value); }
        }

        public string DataSource
        {
            get { return _dataSource; }
            set { SetProperty(ref _dataSource, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        #endregion
    }
}