using Npgsql;
using Microsoft.Extensions.Configuration;

namespace CatsShop.Classes.DataBaseConnection;

/// <summary>
/// Строка подключения к базе данных
/// </summary>
public class DataBaseDatas : DataBaseConnectionText
{
    public DataBaseDatas() : base()
    {
        Host = "localhost";
        Port = 5432;
        Database = "CatsShop";
        UserName = "postgres";
        Password = "password";
    }

    /// <summary>
    /// Получить строку подключения из файла конфигурации
    /// </summary>
    /// <returns></returns>
    public static DataBaseDatas GetConnectionStringFromDatas()
    {
        DataBaseDatas dataBaseDatas = new DataBaseDatas();
        dataBaseDatas.FromSettings();
        return dataBaseDatas;
    }

    /// <summary>
    /// Получить данные строки подключения из файла конфигурации
    /// </summary>
    public void FromSettings()
    {
        
        ConfigurationBuilder bulder = new ConfigurationBuilder();
        bulder.AddJsonFile("appsettings.json");
        IConfigurationSection app = bulder.Build().GetSection("ConnctionString");
        Host = app["host"]?? "localhost";
        string port = app["port"] ?? "5432";
        Port = Convert.ToInt32(port);
        Database = app["database"] ?? "CatsShop";
        UserName = app["username"] ?? "postgres";
        Password = app["password"] ?? "password";

    }

    /// <summary>
    /// Сохранить строку подключения в файл конфигурации
    /// </summary>
    public void SaveSettings()
    {
        ConfigurationBuilder bulder = new ConfigurationBuilder();
        bulder.AddJsonFile("appsettings.json");
        IConfigurationRoot root = bulder.Build();
        IConfigurationSection app = root.GetSection("ConnctionString");
        app["host"] = Host;
        app["port"] = Port.ToString();
        app["database"] = Database;
        app["username"] = UserName;
        app["password"] = Password;
        root.Reload();


        
    }


    public DataBaseDatas(DataBaseConnectionText connectionText) : base(connectionText)
    {
        
    }

    /// <summary>
    /// Получить объект класса без заполненных данных строки подключения
    /// </summary>
    /// <returns></returns>
    public static DataBaseDatas GetDataBaseWithoutConnectionString() => new DataBaseDatas();

    /// <summary>
    /// Получить строку подключения из файла конфигурации
    /// </summary>
    /// <returns></returns>
    public static DataBaseDatas GetDataBase()
    {
        DataBaseDatas dataBaseDatas = new DataBaseDatas();
        //dataBaseDatas.FromSettings();
        return dataBaseDatas;
    }
    
    /// <summary>
    /// Возращает или задаёт объект построителя строки подключения к базе данных
    /// </summary>
    public NpgsqlConnectionStringBuilder ConnectionBuilder
    {
        get => GetConnectionBuilder();
        set => SetConnectionBuilder(value);
    }

    /// <summary>
    /// Возращает или задаёт текст строки подключения
    /// </summary>
    public string ConnectionString
    {
        get
        {
            try
            {
                return ConnectionBuilder.ConnectionString;
            }
            catch (Exception e)
            {
                ConnectionBuilder = new NpgsqlConnectionStringBuilder();
                return ConnectionBuilder.ConnectionString;
            }
        }
        set
        {

            SetConnectionBuilder(new NpgsqlConnectionStringBuilder(value));
        }
    }

    /// <summary>
    /// Возращает или задаёт объект для подключения к базе данных
    /// </summary>
    public NpgsqlConnection Connection
    {
        get => new NpgsqlConnection(ConnectionString);
        set => ConnectionString = value.ConnectionString;
    }

    /// <summary>
    /// Получает данные из построителя строки подключения
    /// </summary>
    /// <param name="builder"></param>
    public void SetConnectionBuilder(NpgsqlConnectionStringBuilder builder)
    {
        NpgsqlConnectionStringBuilder datas = builder;
        Host = datas.Host;
        Port = datas.Port;
        Database = datas.Database;
        UserName = datas.Username;
        Password = datas.Password;
    }

    /// <summary>
    /// Возвращает построитель строки подключения из данных
    /// </summary>
    /// <returns></returns>
    public NpgsqlConnectionStringBuilder GetConnectionBuilder()
    {
        DataBaseDatas datas = this;
        NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder
        {
            Host = datas.Host,
            Port = datas.Port,
            Database = datas.Database,
            Username = datas.UserName,
            Password = datas.Password,
            SslMode = SslMode.Prefer
        };
        return builder;
    }
}