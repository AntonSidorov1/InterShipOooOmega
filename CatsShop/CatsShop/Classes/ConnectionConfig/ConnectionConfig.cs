using Npgsql;
namespace CatsShop
{
    public static class ConnectionConfig
    {
        static NpgsqlConnectionStringBuilder builder;
        public static NpgsqlConnectionStringBuilder Builder { get => Builder; set => builder = value; }


        public static NpgsqlConnection Connection
        {
            get => new NpgsqlConnection(Builder.ConnectionString);
            set => Builder = new NpgsqlConnectionStringBuilder(value.ConnectionString);
        }

        public static void GetConnectionString()
        {
            CreateConnectionString();
        }

        public static NpgsqlConnection GetConnection()
        {
            GetConnection();
            return Connection;
        }

        public static void CreateConnectionString()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionDatabase = "ConnectionDataBase";

            Builder = new NpgsqlConnectionStringBuilder();
            Builder.Host = configuration[$"{connectionDatabase}:Host"];
            Builder.Port = Convert.ToInt32(configuration[$"{connectionDatabase}:Port"]);
            Builder.Database = configuration[$"{connectionDatabase}:DataBase"];
            Builder.Username = configuration[$"{connectionDatabase}:UserName"];
            Builder.Password = configuration[$"{connectionDatabase}:Password"];
        }

    }
}
