using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Sessions;
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

    public CatModel GetModelFromID(int id)
    {
        return GetDatasFromID(id).CopyWithID(id);
    }

    public List<CatModelFullDatas> GetListFullDatas()
    {
        List<CatModelFullDatas> datas = new List<CatModelFullDatas>();
        for(int i =0; i < Count; i++)
        {
            datas.Add(this[i].GetFullDatas());
        }
        return datas;
    }
    
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

    public bool AddModel(CatModelDatasName catModel, string session)
    {
        try
        {
            return AddModel(catModel.Copy(), session);
        }
        catch (Exception e)
        {
            return false;
        }
    }

		
    public bool AddModel(CatModelDatas model, string session)
    {
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();
        try
        {
            if (!SessionsList.GetSessions().UserIsAdmin(session))
                throw new AggregateException("Пользователь должен быть администратор");


            NpgsqlCommand command = new NpgsqlCommand("Insert Into \"CatModel\" " +
                                                      "(\"CatSpeciesID\", \"CatColorID\", \"CatGenderID\") " +
                                                      $"Values ({model.SpeciesID}, {model.ColorID}, {model.GenderID})",
                    connection);
            
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

    
    public bool HaveModelWithID(int id)
    {
        try
        {
            
            GetDatasFromID(id).Copy();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool UpdateModel(CatModelDatasName catModel, string session, int modelID)
    {
        try
        {
            return UpdateModel(catModel.Copy().CopyWithID(modelID), session);
        }
        catch (Exception e)
        {
            return false;
        }
    }
   
    public bool UpdateModel(CatModel model, string session)
    {
        GetModelsFromDB();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();
        try
        {
            if (!SessionsList.GetSessions().UserIsAdmin(session))
                throw new AggregateException("Пользователь должен быть администратор");
            if (!HaveModelWithID(model.ID))
                throw new ArgumentException("Данной модели не существует");
            

            NpgsqlCommand command = new NpgsqlCommand("Update \"CatModel\" " +
                                                      $"set \"CatColorID\" = {model.ColorID} " +
                                                      $"where \"CatModelID\" = {model.ID}", connection);

            command.ExecuteNonQuery();
			
			command = new NpgsqlCommand("Update \"CatModel\" " +
                                                      $"set \"CatGenderID\" = {model.GenderID} " +
                                                      $"where \"CatModelID\" = {model.ID}", connection);

            command.ExecuteNonQuery();

			command = new NpgsqlCommand("Update \"CatModel\" " +
                                                      $"set \"CatSpeciesID\" = {model.SpeciesID} " +
                                                      $"where \"CatModelID\" = {model.ID}", connection);

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

    public bool DeleteModel(int id, string session)
    {
        GetModelsFromDB();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();
        try
        {
            if (!SessionsList.GetSessions().UserIsAdmin(session))
                throw new AggregateException("Пользователь должен быть администратор");
            if (!HaveModelWithID(id))
                throw new ArgumentException("Данной породы не существует");
            

            NpgsqlCommand command = new NpgsqlCommand("Delete From \"CatModel\" " +
                                                      $"where \"CatModelID\" = {id}", connection);
            
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