using CatsShop.Classes.DataBaseConnection;
using CatsShop.Classes.Users.Sessions;
using Npgsql;
using System.Security.Cryptography.X509Certificates;

namespace CatsShop.Classes.Buy
{
    /// <summary>
    /// Список покупок
    /// </summary>
    public class BuysList : List<Buy>
    {

        public static BuysList GetBuys() => new BuysList();


        /// <summary>
        /// Получить список покупок из базы данных
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static BuysList GetBuysListFromDB(string session)
        {
            BuysList list = new BuysList();
            list.GetBuysFromDB(session);
            return list;
        }

        /// <summary>
        /// Получить покупку по её ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Buy GetBuyFromID(int id)
        {
            
            return Find(p => p.ID == id) ?? throw new ArgumentException("Данной покупки не существует");
        }


        /// <summary>
        /// Получить список покупок из базы данных
        /// </summary>
        /// <param name="session"></param>
        public void GetBuysFromDB(string session)
        {
            SessionsList sessionsList = SessionsList.GetSessions();
            string command = $"Select * From \"BuyUser\"";
            if(sessionsList.UserIsKlient(session))
            {
                int user = sessionsList.GetUserIDFromSession(session);
                command += $" where \"UserID\"={user}";
            }
            else if(!sessionsList.UserIsAdmin(session))
            {
                throw new Exception("Пользователь должен быть клиентом или администратором");
            }

            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();

            try
            {
                NpgsqlCommand commandSQL = new NpgsqlCommand(command, connection);
                NpgsqlDataReader reader = commandSQL.ExecuteReader();

                try
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            Buy buy = new Buy
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("BuyID")),
                                Client = reader.GetString(reader.GetOrdinal("UserLogin")),
                                PozitionID = reader.GetInt32(reader.GetOrdinal("BuyPozitionID")),
                                BuyDate = reader.GetDateTime(reader.GetOrdinal("BuyDate"))
                            };
                            Add(buy);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    reader.Close();
                    throw ex;
                }

                connection.Close();
            }
            catch(Exception e)
            {
                connection.Close();
                throw e;
            }
            
            

        }

        

        /// <summary>
        /// Добавить покупку
        /// </summary>
        /// <param name="buy"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public bool AddBuy(int pozitionID, string session)
        {
            DataBaseDatas datas = NowConnectionString.ConnectionDatas;
            NpgsqlConnection connection = datas.Connection;
            connection.Open();
            try
            {

                if(!SessionsList.GetSessions().UserIsKlient(session))
                {
                    throw new ArgumentException("Прользователь должен быть клиентом");
                }

                int user = SessionsList.GetSessions().GetUserIDFromSession(session);

                NpgsqlCommand command = new NpgsqlCommand($"Insert Into \"Buy\" (\"BuyClientID\", \"BuyPozitionID\") " +
                    $"Values ({user}, {pozitionID})", connection);

                command.ExecuteNonQuery();

                connection.Close();
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
