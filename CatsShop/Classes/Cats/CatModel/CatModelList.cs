using CatsShop.Classes.DataBaseConnection;
using Npgsql;

namespace CatsShop.Classes.Cats.CatModel;

public class CatModelList : List<CatModel>
{


    public static CatModelList GetModelsList() => new CatModelList();

    public static CatModelList GetModelsListFromDB()
    {
        CatModelList modelList = GetModelsList();
        modelList.GetModelsFromDB();
        return modelList;
    }

    public CatModelDatas GetDatasFromID(int id)
        => Find(p => p.ID == id).Copy();
    
    public void GetModelsFromDB()
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
                        GenderID = reader.GetInt32(reader.GetOrdinal("CatGenderID")),
                        SpeciesID = reader.GetInt32(reader.GetOrdinal("CatSpeciesID")),
                        ColorID = reader.GetInt32(reader.GetOrdinal("CatColorID"))
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