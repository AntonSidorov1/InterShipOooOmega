using Npgsql;
using Microsoft.Extensions.Configuration;

namespace CatsShop.Classes.DataBaseConnection;

/// <summary>
/// ������ ����������� � ���� ������
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
    /// �������� ������ ����������� �� ����� ������������
    /// </summary>
    /// <returns></returns>
    public static DataBaseDatas GetConnectionStringFromDatas()
    {
        DataBaseDatas dataBaseDatas = new DataBaseDatas();
        dataBaseDatas.FromSettings();
        return dataBaseDatas;
    }

    /// <summary>
    /// �������� ������ ������ ����������� �� ����� ������������
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
    /// ��������� ������ ����������� � ���� ������������
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
    /// �������� ������ ������ ��� ����������� ������ ������ �����������
    /// </summary>
    /// <returns></returns>
    public static DataBaseDatas GetDataBaseWithoutConnectionString() => new DataBaseDatas();

    /// <summary>
    /// �������� ������ ����������� �� ����� ������������
    /// </summary>
    /// <returns></returns>
    public static DataBaseDatas GetDataBase()
    {
        DataBaseDatas dataBaseDatas = new DataBaseDatas();
        //dataBaseDatas.FromSettings();
        return dataBaseDatas;
    }
    
    /// <summary>
    /// ��������� ��� ����� ������ ����������� ������ ����������� � ���� ������
    /// </summary>
    public NpgsqlConnectionStringBuilder ConnectionBuilder
    {
        get => GetConnectionBuilder();
        set => SetConnectionBuilder(value);
    }

    /// <summary>
    /// ��������� ��� ����� ����� ������ �����������
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
    /// ��������� ��� ����� ������ ��� ����������� � ���� ������
    /// </summary>
    public NpgsqlConnection Connection
    {
        get => new NpgsqlConnection(ConnectionString);
        set => ConnectionString = value.ConnectionString;
    }

    /// <summary>
    /// �������� ������ �� ����������� ������ �����������
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
    /// ���������� ����������� ������ ����������� �� ������
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