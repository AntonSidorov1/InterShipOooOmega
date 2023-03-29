using Npgsql;

namespace CatsShop
{
    public class GendersList : List<Gender>
    {
        public static GendersList CreateGenderList() => new GendersList();
        public GendersList GetGendersList() => this;

        public static GendersList CreateGendersListFromDB()
        {
            GendersList gendersList = CreateGenderList();
            gendersList.GetGendersListFromDB();
            return gendersList;
        }

        public void GetGendersListFromDB()
        {
            Clear();

            NpgsqlConnection connection = ConnectionConfig.GetConnection();
            connection.Open();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Select * From \"Gender\"", connection);

                NpgsqlDataReader reader = command.ExecuteReader();

                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Gender gender = new Gender();
                            gender.ID = reader.GetInt32(reader.GetOrdinal("GenderID"));
                            gender.Name = reader.GetString(reader.GetOrdinal("GenderName"));
                            Add(gender);
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

        public Gender? GetGenderFromID(int id) => GetGendersList().FirstOrDefault(gender =>  gender.ID == id);
        public Gender? GetGenderFromName(string name) => GetGendersList().FirstOrDefault(gender => StringNormalize.Normalize(gender.Name) == StringNormalize.Normalize(name));

        public Gender? GetGender(int id) => GetGenderFromID(id);
        public Gender? GetGender(string name) => GetGenderFromName(name);
    }
}
