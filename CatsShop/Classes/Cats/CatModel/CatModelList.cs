using CatsShop.Classes.DataBaseConnection;
using Npgsql;

namespace CatsShop.Classes.Cats.CatModel;

public class CatModelList : List<CatModel>
{


    public static CatModelList GetModelList() => new CatModelList();

    public void GetModelFromDB()
    {
        
        Clear();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Select * From \"CatModel\"", connection);

        
        NpgsqlDataReader reader = command.ExecuteReader();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CatModel species = new CatModel
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("CatModelID")),
                        Name = reader.GetString(reader.GetOrdinal("CatSpeciesName"))
                    };
                    Add(species);
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