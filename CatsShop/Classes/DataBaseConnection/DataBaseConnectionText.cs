namespace CatsShop
{


    public class DataBaseConnectionText
    {



        private string host = "localhost";

        public string Host
        {
            get => host;
            set => host = value;
        }

        private string database = "CatsShop";

        public string Database
        {
            get => database;
            set => database = value;
        }

        private string username = "postgres";

        public string UserName
        {
            get => username;
            set => username = value;
        }

        private string password = "password";

        public string Password
        {
            get => password;
            set => password = value;
        }

        private int port = 5432;

        public int Port
        {
            get => port;
            set => port = value;
        }

        public DataBaseConnectionText()
        {

        }

        public DataBaseConnectionText(DataBaseConnectionText connectionText)
        {
            Host = connectionText.Host;
            Port = connectionText.Port;
            Database = connectionText.Database;
            UserName = connectionText.UserName;
            Password = connectionText.Password;
        }

        public DataBaseConnectionText Copy()
        {
            return new DataBaseConnectionText(this);
        }
    }
}