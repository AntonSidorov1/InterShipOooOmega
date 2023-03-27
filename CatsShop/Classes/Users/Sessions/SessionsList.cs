using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Roles;
using Npgsql;

namespace CatsShop.Classes.Users.Sessions;

/// <summary>
/// Список ключей сессии
/// </summary>
public class SessionsList : List<Key>
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

    public void DeleteOldSession()
    {
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Delete From \"Session\" " +
            "where Extract(Hour From now() - \"TimeOpen\") > 5; ", connection);


        command.ExecuteNonQuery();

        connection.Close();
    }


    /// <summary>
    /// Изменить пароль у аккаунта с данным ключом сессии
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool ChangePassword(string session, string password)
    {
        DeleteOldSession();
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
        DeleteOldSession();
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
        DeleteOldSession();
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
    /// Существует ли данная сессия
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    public bool HaveSession(string session)
    {
        DeleteOldSession();
        try
        {
            GetUserIDFromSession(session);
            return true;
        }
        catch
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
        DeleteOldSession();
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
        DeleteOldSession();
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
        DeleteOldSession();
        Clear();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Select * From \"Session\"" +
                                                  $"where \"SessionUserID\" = {userID}", connection);

        
        NpgsqlDataReader reader = command.ExecuteReader();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Key key = new Key();
                    key.Session = reader.GetString(reader.GetOrdinal("SessionKey"));
                    key.TimeOpen = reader.GetDateTime(reader.GetOrdinal("TimeOpen"));
                    Add(key);
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