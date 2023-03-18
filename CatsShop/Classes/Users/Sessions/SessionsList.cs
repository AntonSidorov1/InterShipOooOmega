using System;
using Npgsql;

namespace CatsShop;

public class SessionsList : List<string>
{

    public static SessionsList GetSessions() => new SessionsList();

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

    public bool ChangePassword(Key key)
        => ChangePassword(key.Session, key.Password);

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

    public void GetSessionsFromDB(string session)
    {
        GetSessionsFromDB(GetUserIDFromSession(session));
    }
    
    
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