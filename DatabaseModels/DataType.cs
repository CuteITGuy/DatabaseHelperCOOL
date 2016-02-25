using System.Data;
using CB.Model.Common;


namespace DatabaseModels
{
    public class DataType: ObservableObject
    {
        #region Fields
        private int? _maximumCharacterLength;
        private DbType _type;
        #endregion


        #region  Properties & Indexers
        public int? MaximumCharacterLength
        {
            get { return _maximumCharacterLength; }
            set { SetProperty(ref _maximumCharacterLength, value); }
        }

        public DbType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
        #endregion


        public override string ToString()
        {
            var s = _type.ToString().ToUpper();
            return MaximumCharacterLength == null
                       ? s : MaximumCharacterLength == -1 ? $"{s}(MAX)" : $"{s}({MaximumCharacterLength})";
        }
    }
}