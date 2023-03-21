namespace CatsShop.Classes.DataBaseConnection
{

    /// <summary>
    /// ������ ����������� � ���� ������
    /// </summary>
    public class DataBaseConnectionText
    {



        private string host = "";

        /// <summary>
        /// ������ ���� ������
        /// </summary>
        public string Host
        {
            get => host;
            set => host = value;
        }

        private string database = "";

        /// <summary>
        /// ���� ���� ������
        /// </summary>
        public string Database
        {
            get => database;
            set => database = value;
        }

        private string username = "";

        /// <summary>
        /// ��� ������������ ���� ������
        /// </summary>
        public string UserName
        {
            get => username;
            set => username = value;
        }

        private string password = "";

        /// <summary>
        /// ������ ��� ����������� � ���� ������
        /// </summary>
        public string Password
        {
            get => password;
            set => password = value;
        }

        private int port = 0;

        /// <summary>
        /// ���� �������, �� ������� ����������� ���� ������
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