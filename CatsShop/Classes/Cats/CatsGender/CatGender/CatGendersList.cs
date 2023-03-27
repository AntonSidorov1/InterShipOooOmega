using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Roles;
using Npgsql;

namespace CatsShop.Classes.Cats.CatsGender.CatGender;

/// <summary>
/// Список полов котиков
/// </summary>
public class CatGendersList : List<CatGender>
{
	public static CatGendersList GetGenderList()
		=> new CatGendersList();

    /// <summary>
    /// Получить список из базы данных
    /// </summary>
    /// <returns></returns>
	public static CatGendersList GetGenderListFromDB()
	{
		CatGendersList genderList = new CatGendersList();
		genderList.GetGendersFromDB();
		return genderList;
	}

    /// <summary>
    /// Получить список из базы данных 
    /// </summary>
    public void GetGendersFromDB()
    {
        
        Clear();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Select * From \"CatGender\"", connection);

        
        NpgsqlDataReader reader = command.ExecuteReader();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CatGender gender = new CatGender();
                    gender.ID = reader.GetInt32(reader.GetOrdinal("CatGenderID"));
                    gender.Name = reader.GetString(reader.GetOrdinal("CatGenderName"));
                    Add(gender);
                }
            }
        }
        catch (Exception e)
        {
            
        }
        reader.Close();
        
        connection.Close();
    }

    /// <summary>
    /// Получить пол по его ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public CatGender GetGenderFromID(int id) => Find(p => p.ID == id);

    /// <summary>
    /// Получить пол по его названию
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public CatGender GetGenderFromName(string name) 
        => Find(p => p.Name.ToLower().Replace('_', ' ').Replace('-', ' ').Trim() == name.ToLower().Replace('_', ' ').Replace('-', ' ').Trim());

}