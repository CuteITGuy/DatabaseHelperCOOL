using CB.Model.Common;


namespace DatabaseModels
{
    public class Column: ObservableObject
    {
        #region Fields
        private DataType _dataType;
        private string _name;
        #endregion


        #region  Properties & Indexers
        private object _defaultValue;

        public object DefaultValue
        {
            get { return _defaultValue; }
            set { SetProperty(ref _defaultValue, value); }
        }

        
        public DataType DataType
        {
            get { return _dataType; }
            set { SetProperty(ref _dataType, value); }
        }

        private bool _isNullable;

        public bool IsNullable
        {
            get { return _isNullable; }
            set { SetProperty(ref _isNullable, value); }
        }

        

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        #endregion
    }
}