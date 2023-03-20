namespace CatsShop.Classes.DataBaseConnection
{


    public class DataBaseConnectionText
    {



        private string host = "";

        public string Host
        {
            get => host;
            set => host = value;
        }

        private string database = "";

        public string Database
        {
            get => database;
            set => database = value;
        }

        private string username = "";

        public string UserName
        {
            get => username;
            set => username = value;
        }

        private string password = "";

        public string Password
        {
            get => password;
            set => password = value;
        }

        private int port = 0;

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