using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.Cats;
using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Sessions;
using Npgsql;

namespace CatsShop.Classes.Position
{
    public class PozitionsList : List<PozitionWithDates>
    {

        public static PozitionsList GetPositionsList() => new PozitionsList();


        public PozitionWithDates GetPositionFromID(int id)
        {
            return Find(p => p.ID == id);
        }

        public bool HavePositionWithID(int id)
        {
            try
            {

                GetPositionFromID(id).Copy();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public Cat GetCatFromPozition(int id)
        {
            return CatsList.GetCatsListFromDB().GetCatFromID(GetPositionFromID(id).CatID);
        }

        public CatModelFullDatas GetCatModelFromPozition(int id)
        {
            return CatModelFullDatas.GetModel(CatModelList.GetModelsListFromDB().GetModelFromID(GetCatFromPozition(id).ModelID));
        }

        public static PozitionsList GetPositionsListFromDB()
        {
            PozitionsList modelList = GetPositionsList();
            modelList.GetPositionsFromDB();
            return modelList;
        }

        public List<PozitionWithDates> GetPositionForCat(int catID) => FindAll(p => p.CatID == catID);



        public void GetPositionsFromDB()
        {

            Clear();
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();

            NpgsqlCommand command = new NpgsqlCommand("Select * From \"Pozition\"", connection);


            NpgsqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PozitionWithDates species = new PozitionWithDates
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("PozitionID")),
                            CatID = reader.GetInt32(reader.GetOrdinal("PozitionCatID")),
                            Cost = reader.GetDecimal(reader.GetOrdinal("PozitionCost")),
                            DateAdded = reader.GetDateTime(reader.GetOrdinal("PozitionDateAdded")),
                            DateOfChanged = reader.GetDateTime(reader.GetOrdinal("PozitionDateOfChanged"))

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


        public bool AddPozition(PozitionDatas model, string session)
        {
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();
            try
            {
                if (!SessionsList.GetSessions().UserIsAdmin(session))
                    throw new AggregateException("Пользователь должен быть администратор");


                NpgsqlCommand command = new NpgsqlCommand("Insert Into \"Pozition\" " +
                                                          "(\"PozitionCatID\", \"PozitionCost\") " +
                                                          $"Values ({model.CatID}, @cost)",
                        connection);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@cost", model.Cost);

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

        public bool UpdatePozition(Pozition position, string session)
        {
            try
            {
                return UpdatePozition(position, session, position.ID);
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePozition(PozitionDatas pozition, string session, int id)
        {
            GetPositionsFromDB();
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();
            try
            {
                if (!SessionsList.GetSessions().UserIsAdmin(session))
                    throw new AggregateException("Пользователь должен быть администратор");
                if (!HavePositionWithID(id))
                    throw new ArgumentException("Данной позиции не существует");

                PozitionWithDates model = pozition.PositionWithDates(id);

                NpgsqlCommand command = new NpgsqlCommand("Update \"Pozition\" " +
                                                      $"set \"PozitionCatID\" = {model.CatID} " +
                                                      $"where \"PozitionID\" = {model.ID}", connection);

                command.ExecuteNonQuery();

                command = new NpgsqlCommand("Update \"Pozition\" " +
                                                          $"set \"PozitionCost\" = @cost " +
                                                          $"where \"PozitionID\" = {model.ID}", connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@cost", model.Cost);

                command.ExecuteNonQuery();

                command = new NpgsqlCommand("Update \"Pozition\" " +
                                                          $"set \"PozitionDateOfChanged\" = @date " +
                                                          $"where \"PozitionID\" = {model.ID}", connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@date", DateTime.Now);

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


        public bool DeletePozition(int id, string session)
        {
            GetPositionsFromDB();
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();
            try
            {
                if (!SessionsList.GetSessions().UserIsAdmin(session))
                    throw new AggregateException("Пользователь должен быть администратор");
                if (!HavePositionWithID(id))
                    throw new ArgumentException("Данной позиции не существует");


                NpgsqlCommand command = new NpgsqlCommand("Delete From \"Pozition\" " +
                                                          $"where \"PozitionID\" = {id}", connection);

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
