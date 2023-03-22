using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Roles;
using Npgsql;

namespace CatsShop.Classes.Users.Sessions;

/// <summary>
/// Список ключей сессии
/// </summary>
public class SessionsList : List<string>
{
    /// <summary>
    /// Получить список
    /// </summary>
    /// <returns></returns>
    public static SessionsList GetSessions() => new SessionsList();

    /// <summary>
    /// Удалить аккаунт с данным ключом сессии
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    public bool DropAccount(string session)
    {
        try
        {
            int userID = GetUserIDFromSession(session);
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();

            NpgsqlCommand command = new NpgsqlCommand("Delete From \"User\" " +
                                                      $"where \"UserID\" = {userID}", connection);
            

            command.ExecuteNonQuery();
        
            connection.Close();

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Изменить пароль у аккаунта с данным ключом сессии
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool ChangePassword(Key key)
        => ChangePassword(key.Session, key.Password);

    /// <summary>
    /// Изменить пароль у аккаунта с данным ключом сессии
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool ChangePassword(string session, string password)
    {
        try
        {
            int userID = GetUserIDFromSession(session);
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();

            NpgsqlCommand command = new NpgsqlCommand("Update \"User\" " +
                                                      "set \"UserPassword\" = @password " +
                                                      $"where \"UserID\" = {userID}", connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@password", password);

            command.ExecuteNonQuery();
        
            connection.Close();

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Пользователь с данным ключом сессии - администратор?
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    public bool UserIsAdmin(string session)
        => GetRoleFromSession(session).Name.ToLower() == ("Администратор").ToLower();

    /// <summary>
    /// Пользователь с данным ключом сессии - клиент?
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    public bool UserIsKlient(string session)
        => GetRoleFromSession(session).Name.ToLower() == ("Клиент").ToLower();

    /// <summary>
    /// Получить роль пользователя по его ключу сессии
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    public Role GetRoleFromSession(string session)
    {
        int userID = GetUserIDFromSession(session);
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Select \"UserRoleID\" From \"User\"" +
                                                  $"where \"UserID\" = '{userID}'", connection);
        
        int roleID = Convert.ToInt32(command.ExecuteScalar());
        
        connection.Close();

        RolesList roles = new RolesList();
        roles.GetRolesFromDB();
        return roles.GetRoleFromID(roleID);

    }

    /// <summary>
    /// Закрыть сессию
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    public bool CloseSessionInDB(string session)
    {
        try
        {
            int userID = GetUserIDFromSession(session);
            
            
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();

            NpgsqlCommand command = new NpgsqlCommand("Delete From \"Session\"" +
                                                      $"where \"SessionKey\" = '{session}'", connection);
            command.ExecuteNonQuery();

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    /// <summary>
    /// Получить логин пользователя по его ключу сессии
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    public string GetLoginFromSession(string session)
    {
        try
        {
            string login = "";
        int userID = GetUserIDFromSession(session);
        
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Select \"UserLogin\" From \"User\"" +
                                                  $"where \"UserID\" = '{userID}'", connection);
        
        login = Convert.ToString(command.ExecuteScalar());
        
        connection.Close();
        
        return login;
        }
        catch (Exception e)
        {
            return "null";
        }
    }

    /// <summary>
    /// Получить ID пользователя по его ключу сессии
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public int GetUserIDFromSession(string session)
    {
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Select Count(*) From \"Session\"" +
                                                  $"where \"SessionKey\" = '{session}'", connection);
        if (Convert.ToInt32(command.ExecuteScalar()) != 1)
        {
            throw new ArgumentException("Данной сессии не существует");
        }
        
        command = new NpgsqlCommand("Select \"SessionUserID\" From \"Session\"" +
                                                  $"where \"SessionKey\" = '{session}'", connection);


        int userID = Convert.ToInt32(command.ExecuteScalar());
        
        connection.Close();
        return userID;
    }

    /// <summary>
    /// Получить список сессий пользователя по его ключу сессии
    /// </summary>
    /// <param name="session"></param>
    public void GetSessionsFromDB(string session)
    {
        GetSessionsFromDB(GetUserIDFromSession(session));
    }
    
    /// <summary>
    /// Получить список сессий пользователя по его ID 
    /// </summary>
    /// <param name="userID"></param>
    public void GetSessionsFromDB(int userID)
    {
        
        Clear();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Select \"SessionKey\" From \"Session\"" +
                                                  $"where \"SessionUserID\" = {userID}", connection);

        
        NpgsqlDataReader reader = command.ExecuteReader();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Add(reader.GetString(reader.GetOrdinal("SessionKey")));
                }
            }
        }
        catch (Exception e)
        {
            
        }
        reader.Close();
        
        connection.Close();
    }
    
    

}