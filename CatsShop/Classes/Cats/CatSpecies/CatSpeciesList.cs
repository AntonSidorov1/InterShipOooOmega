using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Sessions;
using Npgsql;

namespace CatsShop.Classes.Cats.CatSpecies;

public class CatSpeciesList : List<CatSpecies>
{
    public static CatSpeciesList GetSpecies() => new CatSpeciesList();
    
    
    public void GetSpeciesFromDB()
    {
        
        Clear();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Select * From \"CatSpecies\"", connection);

        
        NpgsqlDataReader reader = command.ExecuteReader();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CatSpecies species = new CatSpecies
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("CatSpeciesID")),
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


    public CatSpecies GetSpeciesFromID(int id)
    {
        return Find(p => p.ID == id);
    }

    public CatSpecies GetSpeciesFromName(string name) => Find(p => p.Name == name);
    
    public CatSpecies GetSpeciesFromName(CatSpeciesName name)
    => GetSpeciesFromName(name.Species);

public bool AddSpecies(CatSpeciesName species, string session) 
=> AddSpecies(species.Species, session);

    
    public bool AddSpecies(string speciesName, string session)
    {
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();
        try
        {
            if (!SessionsList.GetSessions().UserIsAdmin(session))
                throw new AggregateException("Пользователь должен быть администратор");
            

            NpgsqlCommand command = new NpgsqlCommand("Insert Into \"CatSpecies\" " +
                                                      "(\"CatSpeciesName\") " +
                                                      "Values (@species)", connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@species", speciesName);

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

    
    public bool HaveSpeciesWithID(int id)
    {
        try
        {
            
            string name = GetSpeciesFromID(id).Name;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    
    public bool UpdateSpecies(CatSpecies species, string session)
    {
        GetSpeciesFromDB();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();
        try
        {
            if (!SessionsList.GetSessions().UserIsAdmin(session))
                throw new AggregateException("Пользователь должен быть администратор");
            if (!HaveSpeciesWithID(species.ID))
                throw new ArgumentException("Данной породы не существует");
            

            NpgsqlCommand command = new NpgsqlCommand("Update \"CatSpecies\" " +
                                                      $"set \"CatSpeciesName\" = @species " +
                                                      $"where \"CatSpeciesID\" = {species.ID}", connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@species", species.Name);

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

    
    public bool DeleteSpecies(int id, string session)
    {
        GetSpeciesFromDB();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();
        try
        {
            if (!SessionsList.GetSessions().UserIsAdmin(session))
                throw new AggregateException("Пользователь должен быть администратор");
            if (!HaveSpeciesWithID(id))
                throw new ArgumentException("Данной породы не существует");
            

            NpgsqlCommand command = new NpgsqlCommand("Delete From \"CatSpecies\" " +
                                                      $"where \"CatSpeciesID\" = {id}", connection);
            
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