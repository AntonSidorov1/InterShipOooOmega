using CatsShop.Classes.Cats.CatsGender.CatGender;
using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Sessions;
using Npgsql;

namespace CatsShop.Classes.Cats.CatColor;

/// <summary>
/// Список цветов котика
/// </summary>
public class CatColorsList : List<CatColor>
{

    /// <summary>
    /// Получить список цветов котика
    /// </summary>
    /// <returns></returns>
    public static CatColorsList GetColors() => new CatColorsList();

    /// <summary>
    /// Получить список цветов из базы данных
    /// </summary>
    /// <returns></returns>
	public static CatColorsList CatColorsListFromDB()
	{
		CatColorsList colorsList = GetColors();
		colorsList.GetColorsFromDB();
		return colorsList;		
	}
    

    /// <summary>
    /// Существует ли цвет с данным ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool HaveColorWithID(int id)
    {
        bool have = true;
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand($"Select Count(*) From \"CatColor\" where \"CatColorID\" = {id}", connection);


        
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

    /// <summary>
    /// Получить список цветов из базы данных
    /// </summary>
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

    public CatColorsList GetCatColors() { return this; }

    /// <summary>
    /// Получить цвет по его ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public CatColor GetColorFromID(int id) => Find(p => p.ID == id);

    /// <summary>
    /// Поулить цвет по его названию
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public CatColor GetColorFromName(string name) => Find(p => p.Name.ToLower() == name.ToLower());

    public bool HaveColor(string name) => GetCatColors().Any(color => color.Name.ToLower() == name.ToLower());

    /// <summary>
    /// Добавить цвет
    /// </summary>
    /// <param name="colorName"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    /// <exception cref="AggregateException"></exception>
    public bool AddColor(string colorName, string session)
    {
        GetColorsFromDB();
        if(HaveColor(colorName)) { return false; }

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

    /// <summary>
    /// Удалить цвет
    /// </summary>
    /// <param name="id"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    /// <exception cref="AggregateException"></exception>
    /// <exception cref="ArgumentException"></exception>
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
    
    /// <summary>
    /// Обновить цвет
    /// </summary>
    /// <returns></returns>
    /// <exception cref="AggregateException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public bool UpdateColor(int id, string colorName, string session)
    {
        GetColorsFromDB();
        if (HaveColor(colorName)) { return false; }
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
            

            NpgsqlCommand command = new NpgsqlCommand("Update \"CatColor\" " +
                                                      $"set \"CatColorName\" = @color " +
                                                      $"where \"CatColorID\" = {id}", connection);
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

}