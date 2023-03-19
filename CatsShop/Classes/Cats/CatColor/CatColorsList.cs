using CatsShop.Classes.Cats.CatsGender.CatGender;
using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Sessions;
using Npgsql;

namespace CatsShop.Classes.Cats.CatColor;

public class CatColorsList : List<CatColor>
{

    public static CatColorsList GetColors() => new CatColorsList();

    public bool HaveColorWithID(int id)
    {
        bool have = true;
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Select Count(*) From \"CatColor\"", connection);


        
        try
        {
            have = Convert.ToInt32(command.ExecuteScalar()) > 0;
        }
        catch (Exception e)
        {
            have = false;
        }
        
        connection.Close();
        return have;
    }
    
    public void GetColorsFromDB()
    {
        
        Clear();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Select * From \"CatColor\"", connection);

        
        NpgsqlDataReader reader = command.ExecuteReader();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CatColor color = new CatColor();
                    color.ID = reader.GetInt32(reader.GetOrdinal("CatColorID"));
                    color.Name = reader.GetString(reader.GetOrdinal("CatColorName"));
                    Add(color);
                }
            }
        }
        catch (Exception e)
        {
            
        }
        reader.Close();
        
        connection.Close();
    }


    public CatColor GetColorFromID(int id) => Find(p => p.ID == id);
    public CatColor GetColorFromName(string name) => Find(p => p.Name == name);

    public bool AddColor(string colorName, string session)
    {
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();
        try
        {
            if (!SessionsList.GetSessions().UserIsAdmin(session))
                throw new AggregateException("Пользователь должен быть администратор");
            

            NpgsqlCommand command = new NpgsqlCommand("Insert Into \"CatColor\" " +
                                                      "(\"CatColorName\") " +
                                                      "Values (@color)", connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@color", colorName);

            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }
        catch (Exception e)
        {
            connection.Close();
            return false;
        }
    }

    public bool DeleteColor(int id, string session)
    {
        GetColorsFromDB();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();
        try
        {
            if (!SessionsList.GetSessions().UserIsAdmin(session))
                throw new AggregateException("Пользователь должен быть администратор");
            if (!HaveColorWithID(id))
                throw new ArgumentException("Данного цвета не существует");
            string name = GetColorFromID(id).Name;
            

            NpgsqlCommand command = new NpgsqlCommand("Delete From \"CatColor\" " +
                                                      $"where \"CatColorID\" = {id}", connection);
            
            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }
        catch (Exception e)
        {
            connection.Close();
            return false;
        }
    }
    
    public bool UpdateColor(CatColor color, string session)
    {
        GetColorsFromDB();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();
        try
        {
            if (!SessionsList.GetSessions().UserIsAdmin(session))
                throw new AggregateException("Пользователь должен быть администратор");
            if (!HaveColorWithID(color.ID))
                throw new ArgumentException("Данного цвета не существует");
            string name = GetColorFromID(color.ID).Name;
            

            NpgsqlCommand command = new NpgsqlCommand("Update \"CatColor\" " +
                                                      $"set \"CatColorName\" = @color " +
                                                      $"where \"CatColorID\" = {color.ID}", connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@color", color.Name);

            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }
        catch (Exception e)
        {
            connection.Close();
            return false;
        }
    }

}