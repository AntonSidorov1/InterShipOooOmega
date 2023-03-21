using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Sessions;
using Npgsql;

namespace CatsShop.Classes.Cats.CatSpecies;

/// <summary>
/// Список пород котиков
/// </summary>
public class CatSpeciesList : List<CatSpecies>
{
    /// <summary>
    /// Получить список пород
    /// </summary>
    /// <returns></returns>
    public static CatSpeciesList GetSpecies() => new CatSpeciesList();
	
    /// <summary>
    /// Получить список пород из базы данных
    /// </summary>
    /// <returns></returns>
	public static CatSpeciesList GetSpeciesListFromDB()
	{
		CatSpeciesList speciesList = GetSpecies();
		speciesList.GetSpeciesFromDB();
		return speciesList;
	}


    /// <summary>
    /// Получить список пород из базы данных
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Получить породу по её ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public CatSpecies GetSpeciesFromID(int id)
    {
        return Find(p => p.ID == id);
    }

    /// <summary>
    /// Получить породу по её названию
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public CatSpecies GetSpeciesFromName(string name) => Find(p => p.Name == name);
    
    public CatSpecies GetSpeciesFromName(CatSpeciesName name)
    => GetSpeciesFromName(name.Species);

    /// <summary>
    /// Добавить породу
    /// </summary>
    /// <param name="species"></param>
    /// <param name="session"></param>
    /// <returns></returns>
	public bool AddSpecies(CatSpeciesName species, string session) 
		=> AddSpecies(species.Species, session);

		/// <summary>
        /// Добавить породу
        /// </summary>
        /// <param name="speciesName"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        /// <exception cref="AggregateException"></exception>
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

    /// <summary>
    /// Сущестует ли порода с данным ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Изменить породу
    /// </summary>
    /// <param name="species"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    /// <exception cref="AggregateException"></exception>
    /// <exception cref="ArgumentException"></exception>
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

    /// <summary>
    /// Удалить породу
    /// </summary>
    /// <param name="id"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    /// <exception cref="AggregateException"></exception>
    /// <exception cref="ArgumentException"></exception>
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