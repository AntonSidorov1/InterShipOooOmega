using Npgsql;

namespace CatsShop
{
    public class UserList : List<UserWithRole>
    {
        public UserList CreateList() => new UserList();
        public UserList GetList() => this;

        public void GetUsersFromDB()
        {
            NpgsqlConnection connection = ConnectionConfig.GetConnection();
            connection.Open();

            
        }

    }
}
