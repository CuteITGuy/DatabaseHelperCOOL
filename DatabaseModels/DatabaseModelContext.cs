using System.Threading.Tasks;


namespace DatabaseModels
{
    public class DatabaseModelContext
    {
        #region Fields
        private readonly string _connectionString;
        #endregion


        #region  Constructors & Destructor
        public DatabaseModelContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion


        #region  Properties & Indexers
        public Server[] Servers { get; set; }
        #endregion


        #region Methods
        public void Load() { }

        public async Task LoadAsync() => await Task.Run(() => Load());
        #endregion
    }
}