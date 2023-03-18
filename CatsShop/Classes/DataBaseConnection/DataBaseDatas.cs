namespace CatsShop;
using System;
using Npgsql;

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

    public DataBaseDatas(DataBaseConnectionText connectionText) : base(connectionText)
    {
        
    }
    
    public static DataBaseDatas GetDataBase() => new DataBaseDatas();
    
    public NpgsqlConnectionStringBuilder ConnectionBuilder
    {
        get => GetConnectionBuilder();
        set => SetConnectionBuilder(value);
    }

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

    public NpgsqlConnection Connection
    {
        get => new NpgsqlConnection(ConnectionString);
        set => ConnectionString = value.ConnectionString;
    }

    public void SetConnectionBuilder(NpgsqlConnectionStringBuilder builder)
    {
        NpgsqlConnectionStringBuilder datas = builder;
        Host = datas.Host;
        Port = datas.Port;
        Database = datas.Database;
        UserName = datas.Username;
        Password = datas.Password;
    }

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