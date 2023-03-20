using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Sessions;
using Npgsql;

namespace CatsShop.Classes.Cats.Cats
{
    public class CatsList : List<Cat>
    {
        
        public static CatsList GetCatsList() => new CatsList();

        public static CatsList GetCatsListFromDB()
        {
            CatsList modelList = GetCatsList();
            modelList.GetCatsFromDB();
            return modelList;
        }

        public CatDatas GetCatDatasFromID(int id)
            => Find(p => p.ID == id).Copy();

        public Cat GetCatFromID(int id)
        {
            return GetCatDatasFromID(id).CopyWithID(id);
        }


        public bool HaveCatWithID(int id)
        {
            try
            {

                GetCatDatasFromID(id).Copy();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public void GetCatsFromDB()
        {

            Clear();
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();

            NpgsqlCommand command = new NpgsqlCommand("Select * From \"Cat\"", connection);


            NpgsqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cat species = new Cat
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("CatID")),
                            Age = reader.GetInt32(reader.GetOrdinal("CatAge")),
                            ModelID = reader.GetInt32(reader.GetOrdinal("CatModelID"))
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


        public bool AddCat(CatDatas model, string session)
        {
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();
            try
            {
                if (!SessionsList.GetSessions().UserIsAdmin(session))
                    throw new AggregateException("Пользователь должен быть администратор");


                NpgsqlCommand command = new NpgsqlCommand("Insert Into \"Cat\" " +
                                                          "(\"CatModelID\", \"CatAge\") " +
                                                          $"Values ({model.ModelID}, {model.Age})",
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

        public bool UpdateCat(Cat cat, string session)
        {
            try
            {
                return UpdateCat(cat, session, cat.ID);
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCat(CatDatas model, string session, int id)
        {
            GetCatsFromDB();
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();
            try
            {
                if (!SessionsList.GetSessions().UserIsAdmin(session))
                    throw new AggregateException("Пользователь должен быть администратор");
                if (!HaveCatWithID(id))
                    throw new ArgumentException("Данного котика не существует");


                NpgsqlCommand command = new NpgsqlCommand("Update \"Cat\" " +
                                                          $"set \"CatModelID\" = {model.ModelID} " +
                                                          $"where \"CatID\" = {id}", connection);

                command.ExecuteNonQuery();

                command = new NpgsqlCommand("Update \"Cat\" " +
                                                          $"set \"CatAge\" = {model.Age} " +
                                                          $"where \"CatID\" = {id}", connection);

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

        public bool DeleteCat(int id, string session)
        {
            GetCatsFromDB();
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();
            try
            {
                if (!SessionsList.GetSessions().UserIsAdmin(session))
                    throw new AggregateException("Пользователь должен быть администратор");
                if (!HaveCatWithID(id))
                    throw new ArgumentException("Данного котика не существует");


                NpgsqlCommand command = new NpgsqlCommand("Delete From \"Cat\" " +
                                                          $"where \"CatID\" = {id}", connection);

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
}
