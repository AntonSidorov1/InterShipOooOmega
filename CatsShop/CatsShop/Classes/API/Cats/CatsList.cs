using Npgsql;
using System.Data;

namespace CatsShop
{
    public class CatsList : List<CatPozition>
    {
        public static CatsList CreateCatList() => new CatsList();
        public CatsList GetCatsList() => this;

        public static CatsList CreateCatsListFromDB()
        {
            CatsList cats = CreateCatList();
            cats.GetCatsListFromDB();
            return cats;
        }

        public void GetCatsListFromDB()
        {
            Clear();

            NpgsqlConnection connection = ConnectionConfig.GetConnection();
            connection.Open();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Select * From \"CatPozition\" where \"Bought\" = FALSE", connection);

                NpgsqlDataReader reader = command.ExecuteReader();

                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CatPozition cat = new CatPozition();
                            cat.SetChanging(false);
                            cat.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                            cat.Color = reader.GetString(reader.GetOrdinal("Color"));
                            cat.Species = reader.GetString(reader.GetOrdinal("Species"));
                            cat.Gender = reader.GetString(reader.GetOrdinal("Gender"));
                            cat.Age = reader.GetInt32(reader.GetOrdinal("Age"));
                            cat.DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded"));
                            cat.DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateChanged"));
                            cat.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
                            cat.SetChanging(true);
                            Add(cat);
                        }
                    }
                }
                catch
                {

                }

                reader.Close();

                connection.Close();
            }
            catch
            {
                connection.Close();
            }
        }

        public Cat? GetCatFromID(int id) => GetCatsList().FirstOrDefault(cat => cat.ID == id);

        public bool CatAdd(Cat cat)
        {
            int gender = 0;
            try
            {
                gender = cat.GetGenderID();
            }
            catch
            {
                return false;
            }


            NpgsqlConnection connection = ConnectionConfig.GetConnection();
            connection.Open();

            int catID = 0;
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Insert Into \"Cat\" " +
                    "(\"CatColor\", \"CatSpecies\", \"CatGenderID\", \"CatAge\") " +
                    $"Values (@color, @species, {gender}, {cat.Age}) returning \"CatID\"", connection);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@color", cat.Color);
                command.Parameters.AddWithValue("@species", cat.Species);
                catID = Convert.ToInt32(command.ExecuteScalar());

            }
            catch
            {
                connection.Close();
                return false;
            }

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Insert Into \"Pozition\" " +
                    "(\"PozitionCatID\", \"PozitionPrice\") " +
                    $"Values ({catID}, @price)", connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@price", cat.Price);
                command.ExecuteNonQuery();

                connection.Close();

                GetCatsListFromDB();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }

        public bool HaveCat(int id) => GetCatsList().Any(cat => cat.ID == id);

        public bool UpdateCat(int id, Cat cat)
        {
            GetCatsListFromDB();
            if (!HaveCat(id)) 
                return false;
            int gender = 0;
            try
            {
                gender = cat.GetGenderID();
            }
            catch
            {
                return false;
            }


            NpgsqlConnection connection = ConnectionConfig.GetConnection();
            connection.Open();


            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Update \"Pozition\" " +
                    $"Set \"PozitionDateChanged\" = @date " +
                    $"where \"PozitionID\" = {id}", connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.ExecuteNonQuery();
                GetCatsListFromDB();
            }
            catch
            {
                connection.Close();
                return false;
            }


            int catID = 0;
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Select \"PozitionCatID\" " +
                    "From \"Pozition\" " +
                    $"where \"PozitionID\" = {id}", connection);
                catID = Convert.ToInt32(command.ExecuteScalar());
                GetCatsListFromDB();
            }
            catch
            {
                connection.Close();
                return false;
            }

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Update \"Cat\" " +
                    $"Set \"CatColor\" = @color, \"CatSpecies\" = @species, \"CatGenderID\" = {gender}, \"CatAge\" = {cat.Age} " +
                    $"where \"CatID\" = {catID}", connection);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@color", cat.Color);
                command.Parameters.AddWithValue("@species", cat.Species);
                command.ExecuteNonQuery();
                GetCatsListFromDB();

            }
            catch
            {
                connection.Close();
                return false;
            }

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Update \"Pozition\" " +
                    $"Set \"PozitionPrice\" = @price, \"PozitionDateChanged\" = @date " +
                    $"where \"PozitionID\" = {id}", connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@price", cat.Price);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.ExecuteNonQuery();

                connection.Close();

                GetCatsListFromDB();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }

        public bool DeleteCat(int id)
        {
            GetCatsListFromDB();
            if (!HaveCat(id)) 
                return false;
            NpgsqlConnection connection = ConnectionConfig.GetConnection();
            connection.Open();

            int catID = 0;
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Select \"PozitionCatID\" " +
                    "From \"Pozition\" " +
                    $"where \"PozitionID\" = {id}", connection);
                catID = Convert.ToInt32(command.ExecuteScalar());
                GetCatsListFromDB();
            }
            catch
            {
                connection.Close();
                return false;
            }

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Delete From \"Cat\" " +
                    $"where \"CatID\" = {catID}", connection);

                
                command.ExecuteNonQuery();
                GetCatsListFromDB();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }

        }
        
        public bool BuyPozition(int id)
        {
            GetCatsListFromDB();
            if (!HaveCat(id))
                return false;
            NpgsqlConnection connection = ConnectionConfig.GetConnection();
            connection.Open();


            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Update \"Pozition\" " +
                    $"Set \"PozitionDateChanged\" = @date " +
                    $"where \"PozitionID\" = {id}", connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.ExecuteNonQuery();
                GetCatsListFromDB();
            }
            catch
            {
                connection.Close();
                return false;
            }


            int catID = 0;
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Update \"Pozition\" " +
                    "set \"PozitionBought\" = @bought, \"PozitionDateChanged\" = @date " +
                    $"where \"PozitionID\" = {id}", connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@bought", true);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.ExecuteNonQuery();

                GetCatsListFromDB();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }
    }
}
