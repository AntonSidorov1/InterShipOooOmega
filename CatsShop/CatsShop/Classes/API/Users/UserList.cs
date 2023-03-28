using Npgsql;

namespace CatsShop
{
    public class UserList : List<UserWithRole>
    {
        public static UserList CreateList() => new UserList();
        public UserList GetList() => this;

        public static UserList CreateUsersFromDB()
        {
            UserList users = CreateList();
            users.GetUsersFromDB();
            return users;
        }

        public bool HaveLogin(User user) => HaveLogin(user.Login);

        public bool HaveLogin(string login)
        {
            return GetList().Any(user => StringNormalize.Normalize(user.Login) == StringNormalize.Normalize(login));
        }

        public void GetUsersFromDB()
        {
            Clear();
            NpgsqlConnection connection = ConnectionConfig.GetConnection();
            connection.Open();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Select * From \"UserRole\"", connection);

                NpgsqlDataReader reader = command.ExecuteReader();

                try
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            UserWithRole user = new UserWithRole();
                            user.Login = reader.GetString(reader.GetOrdinal("Login"));
                            user.Password = reader.GetString(reader.GetOrdinal("Password"));
                            user.RoleRus = reader.GetString(reader.GetOrdinal("Role"));
                            Add(user);
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


        public bool PasswordIsRole(User user) => PasswordIsRole(user.Password);

        public bool PasswordIsRole(string password)
        {
            List<string> roles = new List<string>()
            {
                "Client",
                "Admin",
                "Клиент",
                "Админ"
            };

            return roles.Any(role => StringNormalize.Normalize(role) == StringNormalize.Normalize(password));
        }

        public bool ErrorUsers(User user) => HaveLogin(user) || PasswordIsRole(user);

        public bool AddUser(User user, int roleID)
        {
            if (ErrorUsers(user))
                return false;

            user.Login = StringNormalize.Normalize(user.Login);
            user.Password = user.Password.Trim();

            NpgsqlConnection connection = ConnectionConfig.GetConnection();
            connection.Open();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Insert Into \"User\" " +
                    "(\"UserLogin\", \"UserPassword\", \"UserRoleID\") " +
                    $"Values (@login, @password, {roleID})", connection);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@login", user.Login);
                command.Parameters.AddWithValue("@password", user.Password);
                command.ExecuteNonQuery();

                connection.Close();

                GetUsersFromDB();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }

        public bool DeleteUser(string login)
        {
            if (!HaveLogin(login)) return false;

            NpgsqlConnection connection = ConnectionConfig.GetConnection();
            connection.Open();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Delete From \"User\" " +
                    $"where \"UserLogin\" = @login", connection);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@login", login);
                command.ExecuteNonQuery();

                connection.Close();

                GetUsersFromDB();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }

        public bool ChangePassword(string login, string password)
        {
            if(PasswordIsRole(password)) return false;
            if(!HaveLogin(login)) return false;
            password = password.Trim();

            NpgsqlConnection connection = ConnectionConfig.GetConnection();
            connection.Open();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("Update \"User\" " +
                    "Set \"UserPassword\" = @password " +
                    $"where \"UserLogin\" = @login", connection);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password);
                command.ExecuteNonQuery();

                connection.Close();

                GetUsersFromDB();
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
