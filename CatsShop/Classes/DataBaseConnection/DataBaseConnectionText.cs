namespace CatsShop.Classes.DataBaseConnection
{

    /// <summary>
    /// Строка подключения к базе данных
    /// </summary>
    public class DataBaseConnectionText
    {



        private string host = "";

        /// <summary>
        /// Сервер базы данных
        /// </summary>
        public string Host
        {
            get => host;
            set => host = value;
        }

        private string database = "";

        /// <summary>
        /// Сама база данных
        /// </summary>
        public string Database
        {
            get => database;
            set => database = value;
        }

        private string username = "";

        /// <summary>
        /// Имя пользователя базы данныз
        /// </summary>
        public string UserName
        {
            get => username;
            set => username = value;
        }

        private string password = "";

        /// <summary>
        /// Пароль для подключения к базе данных
        /// </summary>
        public string Password
        {
            get => password;
            set => password = value;
        }

        private int port = 0;

        /// <summary>
        /// Порт сервера, на котором расположена база данных
        /// </summary>
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