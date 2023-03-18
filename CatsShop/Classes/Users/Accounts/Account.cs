using System.Linq.Expressions;
using Npgsql;

namespace CatsShop;

/// <summary>
/// Аккаунт пользователя
/// </summary>
public class Account
{
    private string login = "";

    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login
    {
        get => login;
        set => login = value;
    }
    
    private string password = "";

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password
    {
        get => password;
        set => password = value;
    }

    public bool AddAccountToDB(string session, int roleID)
    {
        try
        {
            Role role = SessionsList.GetSessions().GetRoleFromSession(session);
            if (role.Name.ToLower() != "администратор")
            {
                return false;
            }
            else
            {
                return PutAccountToDB(roleID);
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public bool PutAccountToDB(int roleID = 1)
    {
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        
        connection.Open();
        
        
        try
        {
            
            //NpgsqlCommand

            NpgsqlCommand commandAdd = new NpgsqlCommand("Insert Into \"User\" (\"UserLogin\", \"UserPassword\", \"UserRoleID\") " +
                                                      $"Values (@login, @password, {roleID})", connection);

            NpgsqlParameterCollection parameters = commandAdd.Parameters;
            parameters.Clear();
            parameters.AddWithValue("@login", login);
            parameters.AddWithValue("@password", password);
            commandAdd.ExecuteNonQuery();
            
            connection.Close();
            return true;
        }
        catch
        {

            connection.Close();
            return false;
        }
    }

    public string SignIn()
    {
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        
        connection.Open();
        try
        {
            NpgsqlCommand command = new NpgsqlCommand("Select Count(*) From \"User\" " +
                                                         $"where (\"UserLogin\" = @login and \"UserPassword\" =  @password)", connection);

            NpgsqlParameterCollection parameters = command.Parameters;
            parameters.Clear();
            parameters.AddWithValue("@login", login);
            parameters.AddWithValue("@password", password);
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count != 1)
            {
                throw new AggregateException("Аккаунт отсутствует");
            }

            command = new NpgsqlCommand("Select \"UserID\" From \"User\" " +
            $"where (\"UserLogin\" = @login and \"UserPassword\" =  @password)", connection);

            
            parameters = command.Parameters;
            parameters.Clear();
            parameters.AddWithValue("@login", login);
            parameters.AddWithValue("@password", password);
            
            int userID = Convert.ToInt32(command.ExecuteScalar());

            Random rand = new Random();
            while (true)
            {
                try
                {
                    string key = "";
                    for (int i = 0; i < 20; i++)
                    {
                        int digit = rand.Next(0, 10);
                        key += digit.ToString().Substring(0, 1);
                    }
                    NpgsqlCommand commandAdd = new NpgsqlCommand("Insert Into \"Session\" (\"SessionKey\", \"SessionUserID\") " +
                                                                 $"Values ('{key}', {userID})", connection);

                    commandAdd.ExecuteNonQuery();
                    connection.Close();
                    return key;
                }
                catch (Exception e)
                {
                    
                }
            }

            
        }
        catch (Exception e)
        {
            connection.Close();
            return "null";
        }
    }
}