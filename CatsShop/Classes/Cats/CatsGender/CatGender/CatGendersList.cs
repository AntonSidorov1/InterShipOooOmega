using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Roles;
using Npgsql;

namespace CatsShop.Classes.Cats.CatsGender.CatGender;

/// <summary>
/// ������ ����� �������
/// </summary>
public class CatGendersList : List<CatGender>
{
	public static CatGendersList GetGenderList()
		=> new CatGendersList();

    /// <summary>
    /// �������� ������ �� ���� ������
    /// </summary>
    /// <returns></returns>
	public static CatGendersList GetGenderListFromDB()
	{
		CatGendersList genderList = new CatGendersList();
		genderList.GetGendersFromDB();
		return genderList;
	}

    /// <summary>
    /// �������� ������ �� ���� ������ 
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
    /// �������� ��� �� ��� ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public CatGender GetGenderFromID(int id) => Find(p => p.ID == id);

    /// <summary>
    /// �������� ��� �� ��� ��������
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public CatGender GetGenderFromName(string name) 
        => Find(p => p.Name.ToLower().Replace('_', ' ').Replace('-', ' ').Trim() == name.ToLower().Replace('_', ' ').Replace('-', ' ').Trim());

}